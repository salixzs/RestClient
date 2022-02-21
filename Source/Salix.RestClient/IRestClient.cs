using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient;

/// <summary>
/// Interface to inherit your service client interfaces from to gain access to built in <see cref="HttpMethod"/> extensions.
/// <code>
/// public interface IMyClientImplementation : IRestClient
/// </code>
/// </summary>
public interface IRestClient
{
    /// <summary>
    /// Last executed operation timing.
    /// </summary>
    public TimeSpan CallTime { get; }

    /// <summary>
    /// HttpClient instance created with client. Changing BaseAddress and DefaultHeaders are possible only before first actual call/use.
    /// </summary>
    HttpClient HttpClientInstance { get; }

    /// <summary>
    /// Sends the HTTP request to API service based with given request object.
    /// </summary>
    /// <param name="request">The HTTP method to be used for request.</param>
    Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request);

    /// <summary>
    /// Sends the HTTP request to APi service based on provided Request message.
    /// </summary>
    /// <typeparam name="T">Expected return type (if any).</typeparam>
    /// <param name="request">Fully formed API request object.</param>
    Task<T?> SendHttpRequest<T>(HttpRequestMessage request);

    #region Get<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1 (passing also given headers and content data)
    /// _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new { id = 12 },
    ///     new QueryParameters {{ "page", 1 }},
    ///     requestObject,
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    Task<HttpResponseMessage> GetAsync(string operation, dynamic? pathParameters,
        QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts), optional query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ content data)
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, new QueryParameters {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and optional query parameters.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, new QueryParameters {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12 (+ content data)
    /// _client.GetAsync("/api/operation/{id}", new { id = 12 }, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="data">The data to be sent as request payload.</param>
    Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL, query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation?page=1 (+ content data)
    /// _client.GetAsync("/api/operation", new QueryParameters {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and query parameters.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation?page=1
    /// _client.GetAsync("/api/operation", new QueryParameters {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts).
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON).
    /// <code>
    /// // Calls REST API at /api/operation/12/children
    /// _client.GetAsync("operation/{id}/children", new { id = 12 });
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
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
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameters {{ "page", 1 }}, requestObject, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<T> GetAsync<T>(string operation, dynamic? pathParameters,
        QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts), query parameters and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1 (+ content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameters {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation/12?page=1
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new { id = 12 }, new QueryParameters {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> GetAsync<T>(string operation, dynamic pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL, query parameters and content data.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation?page=1 (+ content data)
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "page", 1 }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data to be sent as request payload.</param>
    Task<T> GetAsync<T>(string operation, QueryParameters queryParameters, object data);

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
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
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
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<T> GetAsync<T>(string operation, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Usually used to perform typed data retrievals from API services.
    /// <code>
    /// // Calls REST API at /api/operation?page=1
    /// _client.GetAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "page", 1 }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data to retrieve.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> GetAsync<T>(string operation, QueryParameters queryParameters);

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
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts) and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child (+ content data)
    /// _client.PostAsync("/api/operation/{id}/child", requestObject, new { id = 12 });
    /// </code>
    /// </summary>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL, query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PostAsync("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data,
        QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and query parameters without sending post content data. Non-standard approach with POST.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (without content data)
    /// _client.PostAsync("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PostAsync(string operation, QueryParameters queryParameters);

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
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
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
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL, query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation?audit=true (+ content data)
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation", requestObject, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, object data, QueryParameters queryParameters);

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
    /// _client.PostAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PostAsync<T>(string operation, QueryParameters queryParameters);

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
    /// _client.PatchAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PatchAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

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
    /// _client.PatchAsync("/api/operation", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data,
        QueryParameters queryParameters);

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
    /// _client.PatchAsync("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PatchAsync(string operation,
        QueryParameters queryParameters);

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
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
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
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

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
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, object data, QueryParameters queryParameters);

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
    /// _client.PatchAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PatchAsync<T>(string operation, QueryParameters queryParameters);

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
    /// _client.PutAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> PutAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data (defaults to JSON) or ignored, if return data is irrelevant.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PutAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

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
    /// _client.PutAsync("/api/operation", requestObject, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data,
        QueryParameters queryParameters);

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
    /// _client.PutAsync("/api/operation/{id}/child", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> PutAsync(string operation, QueryParameters queryParameters);

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
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
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
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters,
        QueryParameters queryParameters);

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
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation", requestObject, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PutAsync<T>(string operation, object data, QueryParameters queryParameters);

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
    /// _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> PutAsync<T>(string operation, QueryParameters queryParameters);

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
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
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
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 }, new QueryParameters {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> DeleteAsync<T>(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data);

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
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> DeleteAsync<T>(string operation, QueryParameters queryParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}/child", new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> DeleteAsync<T>(string operation, dynamic pathParameters, QueryParameters queryParameters);

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
    /// _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Type of data that will be returned.</typeparam>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<T> DeleteAsync<T>(string operation, QueryParameters queryParameters);

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
    /// _client.DeleteAsync("/api/operation/{id}/child", requestObject, new { id = 12 }, new QueryParameters {{ "audit", true }}, new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, object? data, dynamic? pathParameters,
        QueryParameters? queryParameters, Dictionary<string, string>? headers);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts), query parameters and request content data.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true (+ content data)
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 }, new QueryParameters {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters,
        QueryParameters queryParameters, object data);

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
    /// _client.DeleteAsync("/api/operation", new QueryParameters {{ "audit", true }}, requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="data">Data object that should be sent to server.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameters queryParameters, object data);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (with interpolated {value} parts) and query parameters.
    /// Returns specified typed object as operation result.
    /// <code>
    /// // Calls REST API at /api/operation/12/child?audit=true
    /// _client.DeleteAsync("/api/operation/{id}/child", new { id = 12 }, new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, dynamic pathParameters, QueryParameters queryParameters);

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
    /// _client.DeleteAsync("/api/operation", new QueryParameters {{ "audit", true }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request failed. Exception.Data contains details on failure.</exception>
    /// <param name="operation">The operation URL.</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    Task<HttpResponseMessage> DeleteAsync(string operation,
        QueryParameters queryParameters);

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

    #region GetRequestObject
    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12?filter=yes" with "Car" object as content and additional header ["Lang": "EN"].
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new { id = 12 },
    ///     new QueryParameters { { "filter", "yes" } },
    ///     new Dictionary&lt;string, string&gt; { { "Lang", "EN" } });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object to add to request as Content.</param>
    /// <param name="pathParameters">Key=Value(s) to be replaced in interpolated <paramref name="operation"/>.</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    /// <param name="headers">Additional headers to be sent with request.</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object? data,
        dynamic? pathParameters,
        QueryParameters? queryParameters,
        Dictionary<string, string>? headers);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12?filter=yes" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new { id = 12 },
    ///     new QueryParameters { { "filter", "yes" } });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object to add to request as Content.</param>
    /// <param name="pathParameters">Keys:Values to be replaced in interpolated <paramref name="operation"/>.</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        dynamic pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new { id = 12 });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="data">Data object to add to request as Content.</param>
    /// <param name="pathParameters">Keys:Values to be replaced in interpolated <paramref name="operation"/>.</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        dynamic pathParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car?filter=yes" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new QueryParameters { { "filter", "yes" } });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">Data object to add to request as Content.</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data,
        QueryParameters queryParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car",
    ///     new Car { Id = 69, Name = "Aston Martin" });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">Data object to add to request as Content.</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        object data);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12?filter=yes" without setting content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new QueryParameters { { "filter", "yes" } },
    ///     new { id = 12 });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">Keys:Values to be replaced in interpolated <paramref name="operation"/>.</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        QueryParameters queryParameters,
        dynamic pathParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car?filter=yes" without setting content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car",
    ///     new QueryParameters { { "filter", "yes" } });
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        QueryParameters queryParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car" without setting content.
    /// var request = await _api.GetRequestMessage(HttpMethod.Put, "/api/car");
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation);
    #endregion
}
