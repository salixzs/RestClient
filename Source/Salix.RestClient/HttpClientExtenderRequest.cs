using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
#pragma warning disable CA1848, CA2254 // Logger delegates and templates

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,object,PathParameters,QueryParameters,Dictionary{string,string})"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object? data,
        PathParameters? pathParameters,
        QueryParameters? queryParameters,
        Dictionary<string, string>? headers)
    {
        var request = this.CreateRequest(method, operation, pathParameters, queryParameters);
        if (data != null)
        {
            _logger.LogTrace("Adding payload data to API RestClient request.");
            request.Content = new StringContent(await _serializer.SerializeAsync(data), Encoding.UTF8,
                "application/json");
        }

        // Handling BASIC authentication
        if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.Basic)
        {
            _logger.LogTrace("Adding Basic authentication token.");
            var usernamePassword =
                Encoding.ASCII.GetBytes($"{_settings.Authentication.UserName}:{_settings.Authentication.Password}");
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(usernamePassword));
        }

        // Handling External authentication request (Eg. for BEARER)
        if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.External)
        {
            // Try both methods - first async then sync.
            var authKeyValue = await this.GetAuthenticationKeyValueAsync();
            if (string.IsNullOrEmpty(authKeyValue.Key) || string.IsNullOrEmpty(authKeyValue.Value))
            {
                authKeyValue = this.GetAuthenticationKeyValue();
                if (string.IsNullOrEmpty(authKeyValue.Key) || string.IsNullOrEmpty(authKeyValue.Value))
                {
                    _logger.LogWarning(
                        "External authentication method did not return Key/Value to be added to request. Maybe forgot to implement GetAuthenticationKeyValue[Async]() method in your client implementation.");
                }
            }

            if (!string.IsNullOrEmpty(authKeyValue.Key) && !string.IsNullOrEmpty(authKeyValue.Value))
            {
                _logger.LogTrace(
                    $"Adding External authentication token \"{authKeyValue.Key} {authKeyValue.Value}\".");
                request.Headers.Authorization = new AuthenticationHeaderValue(authKeyValue.Key, authKeyValue.Value);
            }
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
        if (!request.Headers.Contains("Accept") &&
            !this.HttpClientInstance.DefaultRequestHeaders.Contains("Accept"))
        {
            _logger.LogTrace("Adding default Accept header for JSON.");
            request.Headers.Add("Accept", "application/json");
        }

        return request;
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
    private HttpRequestMessage CreateRequest(HttpMethod method, string operation, PathParameters? pathParameters, QueryParameters? queryParameters)
        => new(method, this.ComposeFullOperationUrl(operation, pathParameters, queryParameters));

    /// <summary>
    /// Composes the full operation URL with base URL, operation and optional query parameters.
    /// </summary>
    /// <param name="operation">The operation of API (like "api/data").</param>
    /// <param name="pathParameters">The path parameters.</param>
    /// <param name="queryParameters">The query parameters.</param>
    private string ComposeFullOperationUrl(string operation, PathParameters? pathParameters, QueryParameters? queryParameters)
    {
        // Replace all path parameters
        if (pathParameters != null)
        {
            foreach (var (pathParameterName, pathParameterValue) in pathParameters.GetAll())
            {
                _logger.LogTrace($"API URL: Replacing {pathParameterName} with value {pathParameterValue}");
                operation = operation.Replace($"{{{pathParameterName}}}", pathParameterValue);
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
#pragma warning restore CA2254, CA1848 // Logger delegates and templates

    #region overrides
    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,object,PathParameters,QueryParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        PathParameters pathParameters,
        QueryParameters queryParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: data,
            pathParameters: pathParameters,
            queryParameters: queryParameters,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,object,PathParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        PathParameters pathParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: data,
            pathParameters: pathParameters,
            queryParameters: null,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,object,QueryParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        QueryParameters queryParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: data,
            pathParameters: null,
            queryParameters: queryParameters,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,object)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: data,
            pathParameters: null,
            queryParameters: null,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,PathParameters,QueryParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        PathParameters pathParameters,
        QueryParameters queryParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: null,
            pathParameters: pathParameters,
            queryParameters: queryParameters,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,PathParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        PathParameters pathParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: null,
            pathParameters: pathParameters,
            queryParameters: null,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string,QueryParameters)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        QueryParameters queryParameters) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: null,
            pathParameters: null,
            queryParameters: queryParameters,
            headers: null);

    /// <inheritdoc cref="IRestClient.GetRequestMessage(HttpMethod,string)"/>
    public async Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation) =>
        await this.GetRequestMessage(
            method,
            operation: operation,
            data: null,
            pathParameters: null,
            queryParameters: null,
            headers: null);
    #endregion
}
