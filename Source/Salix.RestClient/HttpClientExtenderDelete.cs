using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Delete<T>

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 }, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL, query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, QueryParameterCollection queryParameters, object data)
        => await this.DeleteAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts).
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, dynamic pathParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> DeleteAsync<T>(string operation, QueryParameterCollection queryParameters)
        => await this.DeleteAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<T> DeleteAsync<T>(string operation)
        => await this
            .DeleteAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null,
                headers: null).ConfigureAwait(false);

    #endregion

    #region Delete<HttpResponseMessage>

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.DeleteAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<HttpResponseMessage>(HttpMethod.Delete, operation: operation, data: data,
            pathParameters: pathParameters, queryParameters: queryParameters, headers: headers).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 }, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL, query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.DeleteAsync("/api/operation", new QueryParameterCollection {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameterCollection queryParameters, object data)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, QueryParameterCollection queryParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts).
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true
    /// _client.DeleteAsync("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameterCollection queryParameters)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation
    /// _client.DeleteAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<HttpResponseMessage> DeleteAsync(string operation)
        => await this.DeleteAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion
}
