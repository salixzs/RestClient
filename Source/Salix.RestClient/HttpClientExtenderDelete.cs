using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Delete<HttpResponseMessage>
    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: pathParameters,
            queryParameters: null, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: null,
            queryParameters: queryParameters, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, object data, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: null,
            queryParameters: null, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: pathParameters,
            queryParameters: null, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: null,
            queryParameters: queryParameters, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, CancellationToken cancel = default)
        => await this.DeleteAsync(operation: operation, pathParameters: null,
            queryParameters: null, data: null, headers: null, cancel).ConfigureAwait(false);
    #endregion

    #region Delete<T>

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest<T>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,object,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: null, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters,object,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: null,
            queryParameters: queryParameters, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,object,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, object data, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: null,
            queryParameters: null, data: data, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: null, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.DeleteAsync<T>(operation: operation, pathParameters: null,
            queryParameters: queryParameters, data: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,CancellationToken)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, CancellationToken cancel = default)
        => await this
            .DeleteAsync<T>(operation: operation, pathParameters: null, queryParameters: null,
                data: null, headers: null, cancel).ConfigureAwait(false);

    #endregion
}
