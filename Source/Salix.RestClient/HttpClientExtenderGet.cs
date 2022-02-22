using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Get<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: data,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: data,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, object data, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: null, data: data,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: null,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: null,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, CancellationToken cancel = default)
        => await this
            .GetAsync(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion

    #region Get<T>

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest<T>(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync<T?>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,object,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,object,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, object data, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: null,
            data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,CancellationToken)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, CancellationToken cancel = default)
        => await this
            .GetAsync<T>(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion
}
