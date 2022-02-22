using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Put<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PutAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.PutAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters)
        => await this.PutAsync(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, QueryParameters queryParameters)
        => await this.PutAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data)
        => await this.PutAsync(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.PutAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters)
        => await this.PutAsync(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, QueryParameters queryParameters)
        => await this.PutAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation)
        => await this.PutAsync(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion

    #region Put<T>

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,PathParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,QueryParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, object data)
        => await this
            .PutAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,PathParameters,QueryParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,PathParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, PathParameters pathParameters)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T?> PutAsync<T>(string operation, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string)"/>
    public virtual async Task<T?> PutAsync<T>(string operation)
        => await this
            .PutAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
