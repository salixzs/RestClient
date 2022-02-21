using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Delete<T>

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,object?,dynamic?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,dynamic,QueryParameters,object)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,dynamic,object)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters,object)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, QueryParameters queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,dynamic,QueryParameters)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameters queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,dynamic)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation, QueryParameters queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync{T}(string)"/>
    public virtual async Task<T> DeleteAsync<T>(string operation)
        => await this
            .DeleteAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null,
                headers: null).ConfigureAwait(false);

    #endregion

    #region Delete<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,object?,dynamic?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<HttpResponseMessage>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,dynamic,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,dynamic,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters,object)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameters queryParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,dynamic,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, QueryParameters queryParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,dynamic)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameters queryParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.DeleteAsync(string)"/>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion
}
