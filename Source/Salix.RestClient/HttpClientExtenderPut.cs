using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Put<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PutAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,PathParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,CancellationToken)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, CancellationToken cancel = default)
        => await this.PutAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    #endregion

    #region Put<T>

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default)
        => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,PathParameters,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,PathParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,QueryParameters,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null, cancel).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,CancellationToken)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, CancellationToken cancel = default)
        => await this
            .PutAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null, cancel)
            .ConfigureAwait(false);

    #endregion
}
