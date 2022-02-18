using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Put<HttpResponseMessage>

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PutAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers)
        => await this
            .SendHttpRequest<HttpResponseMessage>(HttpMethod.Put, operation, data, pathParameters, queryParameters,
                headers).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PutAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PutAsync("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PutAsync("/api/operation", requestObject, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data,
        QueryParameterCollection queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PutAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, object data)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: data, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and query parameters, but without request content data.
    /// This is non-standard situation with PUT method.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PutAsync("/api/operation/{id}/child", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation, QueryParameterCollection queryParameters)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and without data. It is non-standard PUT request.
    /// This is non-standard situation with PUT method.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PutAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<HttpResponseMessage> PutAsync(string operation)
        => await this.PutAsync<HttpResponseMessage>(operation: operation, data: null, pathParameters: null,
            queryParameters: null, headers: null).ConfigureAwait(false);

    #endregion

    #region Put<T>

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<T> PutAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: pathParameters,
            queryParameters: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation", requestObject, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> PutAsync<T>(string operation, object data, QueryParameterCollection queryParameters)
        => await this.PutAsync<T>(operation: operation, data: data, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    public virtual async Task<T> PutAsync<T>(string operation, object data)
        => await this
            .PutAsync<T>(operation: operation, data: data, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and query parameters, but without request content data.
    /// This is non-standard situation with PUT method.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> PutAsync<T>(string operation, QueryParameterCollection queryParameters)
        => await this.PutAsync<T>(operation: operation, data: null, pathParameters: null,
            queryParameters: queryParameters, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and without data. It is non-standard PUT request.
    /// This is non-standard situation with PUT method.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<T> PutAsync<T>(string operation)
        => await this
            .PutAsync<T>(operation: operation, data: null, pathParameters: null, queryParameters: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
