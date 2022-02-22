using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Patch<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest(new HttpMethod("PATCH"), operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, CancellationToken cancel = default)
        => await this.PatchAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    #endregion

    #region Patch<T>

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest<T>(new HttpMethod("PATCH"), operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, object data, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PatchAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,CancellationToken)"/>
    public virtual async Task<T?> PatchAsync<T>(string operation, CancellationToken cancel = default)
        => await this
            .PatchAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion
}
