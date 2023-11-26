using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
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
    /// Set Operation Cancellation behaviour when <see cref="CancellationToken"/> is explicitly supplied.<br/>
    /// Default setting: True (will throw <see cref="OperationCanceledException"/>.<br/>
    /// Normally you should allow to throw exception and handle it in your code to avoid weird behaviours.<br/>
    /// On False:<br/>
    /// - Typed operation calls will return default values.<br/>
    /// - HttpResponseMessage operations will return empty object with Status <see cref="HttpStatusCode.ResetContent"/>.
    /// </summary>
    public bool ThrowOnCancellation { get; set; } = true;

    /// <summary>
    /// Method to be overridden in inheriting class to retrieve authentication key-value pair (eg. Bearer Token)
    /// </summary>
    protected virtual async Task<(string Key, string Value)> GetAuthenticationKeyValueAsync() => new("", "");

    /// <summary>
    /// Method to be overridden in inheriting class to retrieve authentication key-value pair (eg. Bearer Token)
    /// </summary>
    protected virtual (string Key, string Value) GetAuthenticationKeyValue() => new("", "");

    /// <summary>
    /// Method to be overridden in inheriting class to append additional header(s) to each request.
    /// </summary>
    protected virtual Dictionary<string, string> GetCommonHeaders() => [];

    private readonly IObjectSerializer _serializer;
    private readonly RestServiceSettings _settings;
    private readonly ILogger _logger;

    /// <summary>
    /// Overridable method to tamper with request right before its getting called.<br/>
    /// See also <see cref="GetAuthenticationKeyValue"/> / <see cref="GetAuthenticationKeyValueAsync"/><br/>and <see cref="GetCommonHeaders"/> for specific global header adding.
    /// </summary>
    /// <param name="request">Prepared request object</param>
    protected virtual void InterceptRequestBeforeCall(HttpRequestMessage request) { return; }

    /// <summary>
    /// Overridable method to handle the response from API to a request after it was issued.<br/>
    /// Use it to handle in some global exception handling or any other logic you want globally for your client to have.<br/>
    /// Return FALSE to prevent client to throw exception (you did handle it yourself) or TRUE (default) for client to throw it.
    /// </summary>
    /// <param name="response">The response object. May be null in case fatal exception was thrown.</param>
    /// <param name="exception">Exception about to be thrown if response returned not success code.<br/>WIll be null for successful responses.<br/>
    /// May be tailored <see cref="RestClientException"/> or some more general exception.
    /// </param>
    /// <returns>Should return TRUE (default) if client should throw exception or false if youhandled exception yourself and do not want client to rethrow it again.</returns>
    protected virtual bool InterceptResponseAfterCall(HttpResponseMessage? response, Exception? exception) => true;

    /// <summary>
    /// The value that indicates the last request's response was success.
    /// </summary>
    public bool? IsSuccessStatusCode { get; private set; }

    /// <summary>
    /// Status code of last request's response code.
    /// </summary>
    public HttpStatusCode? StatusCode { get; private set; }

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
    /// Sends the HTTP request based on given criteria (parameters) and returns RAW HttpResponseMessage.
    /// </summary>
    /// <param name="method">The HTTP method to be used for request.</param>
    /// <param name="operation">The operation URI - resource to be used.</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
    /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    /// <param name="cancellationToken">Async operation cancellation token.</param>
    private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string operation, object? data = null, PathParameters? pathParameters = null, QueryParameters? queryParameters = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var result = new HttpResponseMessage(HttpStatusCode.ResetContent);
        var timer = Stopwatch.StartNew();

        using (var request = await this.GetRequestMessage(method, operation, data, pathParameters, queryParameters, headers))
        {
            _logger.LogDebug($"Calling API {this.HttpClientInstance.BaseAddress} {method} {operation}");

            try
            {
                this.InterceptRequestBeforeCall(request);
                // ========== API Service CALLED HERE!!!! ========
                result = await this.HttpClientInstance
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (OperationCanceledException cancelledException)
            {
                this.StopTimer(timer, true);

                if (result != null)
                {
                    this.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    this.StatusCode = result.StatusCode;
                }

                var doRethrow = this.InterceptResponseAfterCall(result, cancelledException);
                if (this.ThrowOnCancellation && doRethrow)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                this.StopTimer(timer, true);
                if (result != null)
                {
                    this.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    this.StatusCode = result.StatusCode;
                }

                var doRethrow = this.InterceptResponseAfterCall(result, ex);
                if (this.ThrowOnCancellation && doRethrow)
                {
                    throw;
                }
            }
        }

        this.IsSuccessStatusCode = result.IsSuccessStatusCode;
        this.StatusCode = result.StatusCode;

        if (!result.IsSuccessStatusCode)
        {
            if (result.StatusCode == HttpStatusCode.ResetContent && result.RequestMessage == null)
            {
                return result;
            }

            _logger.LogTrace($"API call failed with status code {result.StatusCode}");
            var retException = await this.ComposeRestClientException(result);
            this.StopTimer(timer);

            var doThrow = this.InterceptResponseAfterCall(result, retException);
            if (this.ThrowOnCancellation && doThrow)
            {
                throw retException;
            }
        }

        this.StopTimer(timer);
        _ = this.InterceptResponseAfterCall(result, null);
        return result;
    }

    /// <summary>
    /// Sends the HTTP request based on given criteria (parameters) and returns deserialized {T} object.
    /// </summary>
    /// <typeparam name="T">Expected return type (if any).</typeparam>
    /// <param name="method">The HTTP method to be used for request.</param>
    /// <param name="operation">The operation URI - resource to be used.</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
    /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    /// <param name="cancellationToken">Asynchronous operation cancellation token.</param>
    private async Task<T?> SendHttpRequest<T>(HttpMethod method, string operation, object? data = null, PathParameters? pathParameters = null, QueryParameters? queryParameters = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var result = await this.SendHttpRequest(method, operation, data, pathParameters, queryParameters, headers, cancellationToken);

        // 204 = generally success code, but no results
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            _logger.LogTrace("API call returned empty result");

            // Returns null for classes or nullable value types (and strings), otherwise default value type
            // Lists if explicitly set as type parameter get initialized as empty lists
            return default;
        }

        if (!result.IsSuccessStatusCode)
        {
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
            throw CreateSerializationRestClientException<T>(result, ex);
        }
    }

    /// <inheritdoc cref="IRestClient.SendHttpRequest"/>
    public async Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var result = new HttpResponseMessage(HttpStatusCode.ResetContent);
        var timer = Stopwatch.StartNew();

        using (request)
        {
            _logger.LogDebug($"Calling API {this.HttpClientInstance.BaseAddress} {request.Method} {request.RequestUri}");
            try
            {
                // ========== API Service CALLED HERE!!!! ========
                result = await this.HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                this.StopTimer(timer, true);
                if (this.ThrowOnCancellation)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                this.StopTimer(timer, true);
                throw;
            }
        }

        if (!result.IsSuccessStatusCode)
        {
            if (result.StatusCode == HttpStatusCode.ResetContent && result.RequestMessage == null)
            {
                return result;
            }

            var retException = await this.ComposeRestClientException(result);
            this.StopTimer(timer);
            throw retException;
        }

        this.StopTimer(timer);
        return result;
    }

    /// <inheritdoc cref="IRestClient.SendHttpRequest{T}"/>
    public async Task<T?> SendHttpRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var result = await this.SendHttpRequest(request, cancellationToken);

        // 204 = generally success code, but no results
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            _logger.LogTrace("API call returned empty result");

            // Returns null for classes or nullable value types (and strings), otherwise default value type
            // Lists if explicitly set as type parameter get initialized as empty lists
            return default;
        }

        if (!result.IsSuccessStatusCode)
        {
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
            throw CreateSerializationRestClientException<T>(result, ex);
        }
    }

    /// <summary>
    /// Creates <see cref="RestClientException"/> from failure during result deserialization.
    /// </summary>
    /// <param name="requestResult">Failed response result.</param>
    /// <param name="serializationException">Exception thrown by serializer.</param>
    private static RestClientException CreateSerializationRestClientException<T>(HttpResponseMessage requestResult, Exception serializationException)
    {
        var requestUri = requestResult.RequestMessage?.RequestUri?.AbsoluteUri ?? "---";
        var requestMethod = requestResult.RequestMessage?.Method?.Method ?? "---";
        var errMsg =
            $"Error occurred while deserializing API response to {typeof(T).FullName}.\n"
            + "Make sure you are calling correct operation and deserializing result to correct type.\n"
            + $"Request status code: {(int)requestResult.StatusCode} ({requestResult.StatusCode}).\n"
            + $"{requestMethod} {requestUri}";
        var retException = new RestClientException(errMsg, serializationException);
        retException.Data.Add("Api.Uri", requestUri);
        retException.Data.Add("Api.Method", requestMethod);
        return retException;
    }

    /// <summary>
    /// Creates <see cref="RestClientException"/> from failed Request response.
    /// </summary>
    /// <param name="result">Failed response result.</param>
    private async Task<RestClientException> ComposeRestClientException(HttpResponseMessage result)
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
        return retException;
    }

    /// <summary>
    /// Common things to finalize operation timing.
    /// </summary>
    private void StopTimer(Stopwatch timer, bool wasCancelled = false)
    {
        timer.Stop();
        _logger.LogDebug(wasCancelled
            ? $"API call was cancelled or failed after {timer.Elapsed}"
            : $"API call took {timer.Elapsed}");
        this.CallTime = timer.Elapsed;
    }

    [ExcludeFromCodeCoverage]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"HttClient to {_settings.BaseAddress}";
}
#pragma warning restore CA2254, CA1848 // Logger delegates and templates
