using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Delete<HttpResponseMessage>
    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data)
        => await this.DeleteAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, object data)
        => await this.DeleteAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters, object data)
        => await this.DeleteAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, object data)
        => await this.DeleteAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.DeleteAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters)
        => await this.DeleteAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters)
        => await this.DeleteAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation)
        => await this.DeleteAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);
    #endregion

    #region Delete<T>

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,QueryParameters,object)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,object)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters,object)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,object)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters,QueryParameters)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,PathParameters)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string)"/>
    public virtual async Task<T?> DeleteAsync<T>(string operation)
        => await this
            .DeleteAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null,
                headers: null).ConfigureAwait(false);

    #endregion
}
