using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Get<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: data, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,object)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, object data)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: data,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, object data)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: data,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, object data)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: null, data: data,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: null,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: null,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync(string)"/>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation)
        => await this
            .GetAsync(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null)
            .ConfigureAwait(false);

    #endregion

    #region Get<T>

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters?,QueryParameters?,object?,Dictionary{string,string}?)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,QueryParameters,object)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: data, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,QueryParameters,object)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters, object data)
        => await this.GetAsync<T?>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: data, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,object)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: data, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,object)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: null,
            data: data, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters,QueryParameters)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,PathParameters)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, PathParameters pathParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.GetAsync{T}(string)"/>
    public virtual async Task<T?> GetAsync<T>(string operation)
        => await this
            .GetAsync<T>(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
