using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public abstract partial class HttpClientExtender
{
    #region Get<HttpResponseMessage>

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ headers and content data)
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }}, requestObject, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts), optional query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ content data)
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: data, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and optional query parameters.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: queryParameters,
            data: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12 (+ content data)
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, object data)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: data,
            headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL, query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation?page=1 (+ content data)
    /// _client.GetAsync("/api/operation", new QueryParameterCollection {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameterCollection queryParameters,
        object data)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: data,
            headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and query parameters.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation?page=1
    /// _client.GetAsync("/api/operation", new QueryParameterCollection {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, QueryParameterCollection queryParameters)
        => await this.GetAsync(operation: operation, pathParameters: null, queryParameters: queryParameters, data: null,
            headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts).
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12/children
    /// _client.GetAsync("operation/{id}/children", new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters)
        => await this.GetAsync(operation: operation, pathParameters: pathParameters, queryParameters: null, data: null,
            headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation
    /// _client.GetAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<HttpResponseMessage> GetAsync(string operation)
        => await this
            .GetAsync(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null)
            .ConfigureAwait(false);

    #endregion

    #region Get<T>

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc headers and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ headers and content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }}, requestObject, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    public virtual async Task<T> GetAsync<T>(string operation, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, object? data, Dictionary<string, string>? headers)
        => await this.SendHttpRequest<T>(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers)
            .ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts), query parameters and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: data, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameterCollection {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters,
            queryParameters: queryParameters, data: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL, query parameters and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation?page=1 (+ content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<T> GetAsync<T>(string operation, QueryParameterCollection queryParameters, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: data, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12 (+ content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="data">The data to be sent as request payload.</param>
    public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters, object data)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: data, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts).
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: pathParameters, queryParameters: null,
            data: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation?page=1
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    public virtual async Task<T> GetAsync<T>(string operation, QueryParameterCollection queryParameters)
        => await this.GetAsync<T>(operation: operation, pathParameters: null, queryParameters: queryParameters,
            data: null, headers: null).ConfigureAwait(false);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    public virtual async Task<T> GetAsync<T>(string operation)
        => await this
            .GetAsync<T>(operation: operation, pathParameters: null, queryParameters: null, data: null, headers: null)
            .ConfigureAwait(false);

    #endregion
}
