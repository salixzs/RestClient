using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Put<HttpResponseMessage>

    /// <inheritdoc cref="IRestClient.PutAsync(string,object?,dynamic?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this
            .SendHttpRequest<HttpResponseMessage>(HttpMethod.Put, operation, data, pathParameters, queryParameters,
                headers).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,dynamic,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,dynamic)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data,
        QueryParameters queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,object)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string,QueryParameters)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, QueryParameters queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync(string)"/>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion

    #region Put<T>

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object?,dynamic?,QueryParameters?,Dictionary{string,string}?)"/>
    public virtual async Task<T> PutAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,dynamic,QueryParameters)"/>
    public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,dynamic)"/>
    public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object,QueryParameters)"/>
    public virtual async Task<T> PutAsync<T>(string operation, object data, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,object)"/>
    public virtual async Task<T> PutAsync<T>(string operation, object data)
        => await this
            .PutAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string,QueryParameters)"/>
    public virtual async Task<T> PutAsync<T>(string operation, QueryParameters queryParameters)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <inheritdoc cref="IRestClient.PutAsync{T}(string)"/>
    public virtual async Task<T> PutAsync<T>(string operation)
        => await this
            .PutAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
