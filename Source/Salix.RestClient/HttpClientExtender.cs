using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

#pragma warning disable CA1848, CA2254 // Logger delegates and templates
/// <summary>
/// Generic, typed REST (API) Service client instance.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract partial class HttpClientExtender
{
    /// <summary>
    /// HttpClient instance created with client. Changing BaseAddress and DefaultHeaders are possible only before first actual call/use.
    /// </summary>
    public HttpClient HttpClientInstance { get; protected set; } = null!;

    /// <summary>
    /// Method to be overriden in inheriting class to retrieve authentication key-value pair (eg. Bearer Token)
    /// </summary>
    protected virtual (string Key, string Value) GetAuthenticationKeyValue() => new("", "");

    private readonly IObjectSerializer _serializer;
    private readonly RestServiceSettings _settings;
    private readonly ILogger _logger;

    /// <summary>
    /// Last operation timing.
    /// </summary>
    public TimeSpan CallTime { get; private set; }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected HttpClientExtender(RestServiceSettings settings, ILogger logger) : this(settings, logger, null)
    {
    }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
#pragma warning disable CS8618 // HttpClientInstance is created by derived class.
    protected HttpClientExtender(RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer)
#pragma warning restore CS8618
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger;
        _serializer = serializer ?? NewtonsoftJsonObjectSerializer.Default;
        _logger.LogDebug($"Created API RestClient to {_settings.BaseAddress}");
    }

    /// <summary>
    /// Sends the HTTP request based on given criteria (parameters).
    /// </summary>
    /// <param name="method">The HTTP method to be used for request.</param>
    /// <param name="operation">The operation URI - resource to be used.</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
    /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string operation, object? data = null, dynamic? pathParameters = null, QueryParameterCollection? queryParameters = null, Dictionary<string, string>? headers = null)
    {
        HttpResponseMessage result;
        var timer = Stopwatch.StartNew();
        using (HttpRequestMessage request = this.CreateRequest(method, operation, pathParameters, queryParameters))
        {
            if (data != null)
            {
                _logger.LogTrace("Adding payload data to API RestClient request.");
                request.Content = new StringContent(await _serializer.SerializeAsync(data), Encoding.UTF8, "application/json");
            }

            // Handling BASIC authentication
            if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.Basic)
            {
                _logger.LogTrace("Adding Basic authentication token.");
                var usernamePassword = Encoding.ASCII.GetBytes($"{_settings.Authentication.UserName}:{_settings.Authentication.Password}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(usernamePassword));
            }

            // Handling External authentication request (Eg. for BEARER)
            if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.External)
            {
                var (authenticationKey, authenticationValue) = this.GetAuthenticationKeyValue();
                if (string.IsNullOrEmpty(authenticationKey) || string.IsNullOrEmpty(authenticationValue))
                {
                    _logger.LogWarning("External authentication method did not return Key/Value to be added to request. Maybe forgot to implement GetAuthenticationKeyValue() method in your client implementation.th");
                }

                _logger.LogTrace($"Adding External authentication token \"{authenticationKey} {authenticationValue}\".");
                request.Headers.Authorization = new AuthenticationHeaderValue(authenticationKey, authenticationValue);
            }

            if (headers is { Count: > 0 })
            {
                foreach (var hdr in headers)
                {
                    if (request.Headers.Contains(hdr.Key))
                    {
                        _logger.LogTrace($"Changing value of request header {hdr.Key} in API RestClient request.");
                        request.Headers.Remove(hdr.Key);
                        request.Headers.Add(hdr.Key, hdr.Value);
                    }
                    else
                    {
                        _logger.LogTrace($"Adding request header {hdr.Key} to API RestClient request.");
                        request.Headers.Add(hdr.Key, hdr.Value);
                    }
                }
            }

            // Demand JSON by default, if not specified otherwise
            if (!request.Headers.Contains("Accept") && !this.HttpClientInstance.DefaultRequestHeaders.Contains("Accept"))
            {
                _logger.LogTrace("Adding default Accept header for JSON.");
                request.Headers.Add("Accept", "application/json");
            }

            // CALLING THE SERVICE HERE!!!
            _logger.LogDebug($"Calling API {this.HttpClientInstance.BaseAddress} {method} {operation}");
            result = await this.HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogTrace($"API call failed with status code {result.StatusCode}");
            var contentString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

            var requestUri = result.RequestMessage.RequestUri?.AbsoluteUri;
            var errMsg =
                "Error occurred in API/Service."
                + $"Request status code: {(int)result.StatusCode} ({result.StatusCode}).\n"
                + $"{result.RequestMessage.Method.Method} {requestUri}";
            var retException = new RestClientException(errMsg)
            {
                ReasonPhrase = result.ReasonPhrase,
                StatusCode = result.StatusCode,
                Method = result.RequestMessage.Method,
                ResponseContent = contentString
            };
            retException.Data.Add("Api.Uri", requestUri);
            retException.Data.Add("Api.StatusCode", result.StatusCode);
            retException.Data.Add("Api.RawError", contentString);
            retException.Data.Add("Api.Method", result.RequestMessage.Method.Method);

            this.StopTimer(timer);
            throw retException;
        }

        this.StopTimer(timer);
        return result;
    }

    /// <summary>
    /// Sends the HTTP request based on given criteria (parameters).
    /// </summary>
    /// <typeparam name="T">Expected return type (if any).</typeparam>
    /// <param name="method">The HTTP method to be used for request.</param>
    /// <param name="operation">The operation URI - resource to be used.</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
    /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    private async Task<T?> SendHttpRequest<T>(HttpMethod method, string operation, object? data = null, dynamic? pathParameters = null, QueryParameterCollection? queryParameters = null, Dictionary<string, string>? headers = null)
    {
        HttpResponseMessage result = await this.SendHttpRequest(method, operation, data, pathParameters, queryParameters, headers);

        // 204 = generally success code, but no results
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            _logger.LogTrace("API call returned empty result");

            // Returns null for classes or nullable value types (and strings), otherwise default value type
            // Lists if explicitly set as type parameter get initialized as empty lists
            return default;
        }

        var contentString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        try
        {
            var returnValue = await _serializer.DeserializeAsync<T>(contentString);
            return returnValue;
        }
        catch (Exception ex)
        {
            var requestUri = result.RequestMessage?.RequestUri?.AbsoluteUri ?? "---";
            var requestMethod = result.RequestMessage?.Method?.Method ?? "---";
            var errMsg =
                $"Error occurred while deserializing API response to {typeof(T).FullName}.\n"
                + "Make sure you are calling correct operation and deserializing result to correct type.\n"
                + $"Request status code: {(int)result.StatusCode} ({result.StatusCode}).\n"
                + $"{requestMethod} {requestUri}";
            var retException = new RestClientException(errMsg, ex);
            retException.Data.Add("Api.Uri", requestUri);
            retException.Data.Add("Api.Method", requestMethod);
            throw retException;
        }
    }

    /// <summary>
    /// Creates the HTTP request for API client to execute.
    /// </summary>
    /// <param name="method">The HTTP method to use for request.</param>
    /// <param name="operation">The operation (base URL).</param>
    /// <param name="pathParameters">The path parameters (replacing placeholders in base path).</param>
    /// <param name="queryParameters">The query parameters - added after question mark in URL.</param>
    /// <returns>
    /// A formed API Request to execute through HttpClient.
    /// </returns>
    protected virtual HttpRequestMessage CreateRequest(HttpMethod method, string operation, dynamic pathParameters, QueryParameterCollection queryParameters)
        => new(method, this.ComposeFullOperationUrl(operation, pathParameters, queryParameters));

    /// <summary>
    /// Composes the full operation URL with base URL, operation and optional query parameters.
    /// </summary>
    /// <param name="operation">The operation of API (like "api/data").</param>
    /// <param name="pathParameters">The path parameters.</param>
    /// <param name="queryParameters">The query parameters.</param>
    private string ComposeFullOperationUrl(string operation, dynamic? pathParameters, QueryParameterCollection? queryParameters)
    {
        // Replace all path parameters
        if (pathParameters != null)
        {
            foreach (PropertyInfo property in pathParameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                _logger.LogTrace($"API URL: Replacing {property.Name} with value {property.GetValue(pathParameters, null).ToString()}");
                operation = operation.Replace($"{{{property.Name}}}", property.GetValue(pathParameters, null).ToString());
            }
        }

        // Add query parameters
        if (queryParameters != null)
        {
            _logger.LogTrace($"API URL: Adding query parameters: {queryParameters}");
            operation = $"{operation}?{queryParameters}";
        }

        return operation;
    }

    /// <summary>
    /// Common things to finalize operation timing.
    /// </summary>
    private void StopTimer(Stopwatch timer)
    {
        timer.Stop();
        _logger.LogDebug($"API call took {timer.Elapsed}");
        this.CallTime = timer.Elapsed;
    }

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => $"HttClient to {_settings.BaseAddress}";
}
#pragma warning restore CA2254, CA1848 // Logger delegates and templates
