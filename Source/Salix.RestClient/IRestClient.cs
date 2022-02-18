using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

public interface IRestClient
{
    /// <summary>
    /// Last operation timing.
    /// </summary>
    public TimeSpan CallTime { get; }

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
    Task<HttpResponseMessage> GetAsync(string operation, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, object? data, Dictionary<string, string>? headers);

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
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data);

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
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, object data);

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
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameterCollection queryParameters, object data);

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
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters);

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
    Task<HttpResponseMessage> GetAsync(string operation);
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
    Task<T> GetAsync<T>(string operation, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, object? data, Dictionary<string, string>? headers);

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
    Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data);

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
    Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters);

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
    Task<T> GetAsync<T>(string operation, QueryParameterCollection queryParameters, object data);

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
    Task<T> GetAsync<T>(string operation, dynamic pathParameters, object data);

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
    Task<T> GetAsync<T>(string operation, dynamic pathParameters);

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
    Task<T> GetAsync<T>(string operation, QueryParameterCollection queryParameters);

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
    Task<T> GetAsync<T>(string operation);
    #endregion

    #region Post<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL, query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PostAsync("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and query parameters without sending post content data. Non-standard approach with POST.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PostAsync("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PostAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation and without request content data. Non-standard approach with POST.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PostAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    Task<HttpResponseMessage> PostAsync(string operation);
    #endregion

    #region Post<T>
    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<T> PostAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL, query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation", requestObject, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, object data, QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and query parameters without content data. This is non-standard POST call.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    Task<T> PostAsync<T>(string operation, object data);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation without request content data. This is non-standard POST call.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    Task<T> PostAsync<T>(string operation);
    #endregion

    #region Patch<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PatchAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PatchAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PatchAsync("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PatchAsync("/api/operation", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PatchAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, query parameters but without request content data.
    /// This is non-standard situation with PATCH method.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PatchAsync("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, but without request content data.
    /// This is non-standard situation with PATCH method.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PatchAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    Task<HttpResponseMessage> PatchAsync(string operation);
    #endregion

    #region Patch<T>
    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts) and optional query parameters, ad-hoc header setting and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ headers and content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<T> PatchAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation", requestObject, new { id = 12 }, new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, object data, QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (+ content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    Task<T> PatchAsync<T>(string operation, object data);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, query parameters but without request content data.
    /// This is non-standard situation with PUT method.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation", new QueryParameterCollection {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, QueryParameterCollection queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, but without request content data.
    /// This is non-standard situation with PUT method.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation (without content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    Task<T> PatchAsync<T>(string operation);
    #endregion

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
    Task<HttpResponseMessage> PutAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

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
    Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters);

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
    Task<HttpResponseMessage> PutAsync(string operation, object data,
        QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> PutAsync(string operation, object data);

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
    Task<HttpResponseMessage> PutAsync(string operation, QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> PutAsync(string operation);
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
    Task<T> PutAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

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
    Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameterCollection queryParameters);

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
    Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters);

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
    Task<T> PutAsync<T>(string operation, object data, QueryParameterCollection queryParameters);

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
    Task<T> PutAsync<T>(string operation, object data);

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
    Task<T> PutAsync<T>(string operation, QueryParameterCollection queryParameters);

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
    Task<T> PutAsync<T>(string operation);
    #endregion

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
    Task<T> DeleteAsync<T>(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

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
    Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data);

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
    Task<T> DeleteAsync<T>(string operation, dynamic pathParameters, object data);

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
    Task<T> DeleteAsync<T>(string operation, QueryParameterCollection queryParameters, object data);

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
    Task<T> DeleteAsync<T>(string operation, dynamic pathParameters);

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
    Task<T> DeleteAsync<T>(string operation, QueryParameterCollection queryParameters);

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
    Task<T> DeleteAsync<T>(string operation);
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
    Task<HttpResponseMessage> DeleteAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameterCollection? queryParameters, Dictionary<string, string>? headers);

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
    Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters,
        QueryParameterCollection queryParameters, object data);

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
    Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, object data);

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
    Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameterCollection queryParameters, object data);

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
    Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters);

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
    Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameterCollection queryParameters);

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
    Task<HttpResponseMessage> DeleteAsync(string operation);
    #endregion
}
