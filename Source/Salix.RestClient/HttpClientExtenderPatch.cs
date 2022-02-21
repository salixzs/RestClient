using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Patch<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object? data, PathParameters? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<HttpResponseMessage>(new HttpMethod("PATCH"), operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters,
        QueryParameters queryParameters)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,PathParameters)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data,
        QueryParameters queryParameters)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation, object data)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation,
        QueryParameters queryParameters)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync(string)"/>
    public virtual async Task<HttpResponseMessage> PatchAsync(string operation)
        => await this.PatchAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion

    #region Patch<T>

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object?,PathParameters?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, object? data, PathParameters? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(new HttpMethod("PATCH"), operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,PathParameters,QueryParameters)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, object data, PathParameters pathParameters,
        QueryParameters queryParameters)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,PathParameters)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, object data, PathParameters pathParameters)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object,QueryParameters)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, object data, QueryParameters queryParameters)
        => await this.PatchAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,object)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, object data)
        => await this
            .PatchAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T> PatchAsync<T>(string operation, QueryParameters queryParameters)
        => await this.PatchAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PatchAsync{T}(string)"/>
    public virtual async Task<T> PatchAsync<T>(string operation)
        => await this
            .PatchAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
