using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Post<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PostAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object? data, PathParameters? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters,
        QueryParameters queryParameters)
        => await this.PostAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters)
        => await this.PostAsync(operation: operation, data: data, pathParameters: pathParameters, queryParameters: null,
            headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data,
        QueryParameters queryParameters)
        => await this.PostAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, QueryParameters queryParameters)
        => await this.PostAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data)
        => await this
            .PostAsync(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync(string)"/>
    public virtual async Task<HttpResponseMessage> PostAsync(string operation)
        => await this
            .PostAsync(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion

    #region Post<T>

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<T> PostAsync<T>(string operation, object? data, PathParameters? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<T> PostAsync<T>(string operation, object data, PathParameters pathParameters,
        QueryParameters queryParameters)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,QueryParameters)"/>
    public virtual async Task<T> PostAsync<T>(string operation, object data, QueryParameters queryParameters)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object,PathParameters)"/>
    public virtual async Task<T> PostAsync<T>(string operation, object data, PathParameters pathParameters)
        => await this.PostAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T> PostAsync<T>(string operation, QueryParameters queryParameters)
        => await this.PostAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string,object)"/>
    public virtual async Task<T> PostAsync<T>(string operation, object data)
        => await this
            .PostAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PostAsync{T}(string)"/>
    public virtual async Task<T> PostAsync<T>(string operation)
        => await this
            .PostAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
