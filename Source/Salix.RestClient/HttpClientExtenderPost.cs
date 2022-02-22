using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Post<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PostAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: data, pathParameters: pathParameters, queryParameters: null,
            headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, CancellationToken cancel = default)
        => await this
            .PostAsync(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, CancellationToken cancel = default)
        => await this
            .PostAsync(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion

    #region Post<T>

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest<T>(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, object data, CancellationToken cancel = default)
        => await this
            .PostAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PostAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,CancellationToken)"/>
    public virtual async Task<T?> PostAsync<T>(string operation, CancellationToken cancel = default)
        => await this
            .PostAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion
}
