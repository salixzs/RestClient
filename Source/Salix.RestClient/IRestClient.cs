using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
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
    /// The value that indicates whether the last request's response was a success.
    /// </summary>
    public bool IsSuccessStatusCode { get; }

    /// <summary>
    /// Status code of last request's response code.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Sends the HTTP request to API service based with given Request message.
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="request">The HTTP method to be used for request.</param>
    /// <param name="cancellationToken">Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends the HTTP request to API service based on given Request message.
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <typeparam name="T">Expected return type. May be null for nullable types if response is NoContent type.</typeparam>
    /// <param name="request">Fully formed API request object.</param>
    /// <param name="cancellationToken">Asynchronous operation cancellation token.</param>
    Task<T?> SendHttpRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken = default);

    #region Get<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1 (passing also given headers and content data)
    /// var response = _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1),
    ///     requestObject,
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1 (passing object as content)
    /// var response = _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12 (passing object as content)
    /// var response = _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12 (passing object as content)
    /// var response = _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and object as content data.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation (passing object as content)
    /// var response = _client.GetAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1
    /// var response = _client.GetAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and query parameters.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation?page=1
    /// var response = _client.GetAsync("/api/operation/{id}", new QueryParameters("page", 1));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation/12
    /// var response = _client.GetAsync("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).
    /// <code>
    /// // Makes an API call to GET /api/operation
    /// var response = _client.GetAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> GetAsync(string operation, CancellationToken cancel = default);
    #endregion

    #region Get<T>
    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request<br/>
    ///   - request content data (request object).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1 (passing also given headers and object as content data)
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1),
    ///     requestObject,
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1 (passing object as content data)
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation?page=1 (passing object as content data)
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     new QueryParameters("page", 1),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and object as content data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation/12 (passing object as content data)
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and object as content data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation (passing object as content data)
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation/12?page=1
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("page", 1));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation/12
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation?page=1
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}", new QueryParameters("page", 1));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default);


    /// <summary>
    /// Performs Asynchronous HTTP GET operation with specified operation URL.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to GET /api/operation
    /// DomainObject result = _client.GetAsync&lt;DomainObject&gt;("/api/operation/{id}");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> GetAsync<T>(string operation, CancellationToken cancel = default);
    #endregion

    #region Post<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true (passing also given headers and content data)
    /// var response = _client.PostAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true (passing also content data)
    /// var response = _client.PostAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and object as request content data.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12 (passing also content data)
    /// var response = _client.PostAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation?audit=true (passing also content data)
    /// var response = _client.PostAsync(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and object as content data.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation (passing also content data)
    /// var response = _client.PostAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true
    /// var response = _client.PostAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12
    /// var response = _client.PostAsync("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and query parameters.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation?audit=true
    /// var response = _client.PostAsync("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation
    /// var response = _client.PostAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PostAsync(string operation, CancellationToken cancel = default);
    #endregion

    #region Post<T>
    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true (passing also given headers and content data)
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true (passing also content data)
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation?audit=true (passing also content data)
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and data object as request content data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12 (passing also content data)
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and data object as request content data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation (passing also content data)
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12?audit=true
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation?audit=true
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation/12
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP POST operation with specified operation URL.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// NOTE: This is non-standard approach with POST, as usually it should get some content data in request.<br/>
    /// <code>
    /// // Makes an API call to POST /api/operation
    /// DomainObject response = _client.PostAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PostAsync<T>(string operation, CancellationToken cancel = default);
    #endregion

    #region Patch<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true (passing also given headers and content data)
    /// var response = _client.PatchAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true (passing also content data object)
    /// var response = _client.PatchAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and request content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12 (passing also content data object)
    /// var response = _client.PatchAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, query parameters and request content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation?audit=true (passing also content data object)
    /// var response = _client.PatchAsync(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and request content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation (passing also content data object)
    /// var response = _client.PatchAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true
    /// var response = _client.PatchAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12
    /// var response = _client.PatchAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and query parameters.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation?audit=true
    /// var response = _client.PatchAsync("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation
    /// var response = _client.PatchAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PatchAsync(string operation, CancellationToken cancel = default);
    #endregion

    #region Patch<T>
    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true (passing also given headers and content data)
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true (passing also content data object)
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12 (passing also content data object)
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL, query parameters and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation?audit=true (passing also content data object)
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation (passing also content data object)
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12?audit=true
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation/12
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL and query parameters.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation?audit=true
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PATCH operation with specified operation URL.<br/>
    /// NOTE: This is non-standard approach with PATCH, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PATCH /api/operation
    /// DomainObject response = _client.PatchAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PatchAsync<T>(string operation, CancellationToken cancel = default);
    #endregion

    #region Put<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true (passing also given headers and content data)
    /// var response = _client.PutAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true (passing also content data object)
    /// var response = _client.PutAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12 (passing also content data object)
    /// var response = _client.PutAsync(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation?audit=true (passing also content data object)
    /// var response = _client.PutAsync(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation (passing also content data object)
    /// var response = _client.PutAsync("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true
    /// var response = _client.PutAsync(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12
    /// var response = _client.PutAsync("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and query parameters.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation?audit=true
    /// var response = _client.PutAsync("/api/operation/{id}", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// Can be used with XML SOAP services to be able to retrieve non-JSON data (default is JSON).<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation
    /// var response = _client.PutAsync("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> PutAsync(string operation, CancellationToken cancel = default);
    #endregion

    #region Put<T>

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true (passing also given headers and content data)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, object? data, PathParameters? pathParameters, QueryParameters? queryParameters, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - request content data (request object).<br/>
    ///   - query parameters<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true (passing also content data object)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12 (passing also content data object)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     requestObject,
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, object data, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL, query parameters and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation?audit=true (passing also content data object)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     requestObject,
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, object data, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation (passing also content data object)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12?audit=true
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation/12 (passing also content data object)
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL and query parameters.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation?audit=true
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP PUT operation with specified operation URL.<br/>
    /// NOTE: This is non-standard approach with PUT, as usually it should get some content data in request.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to PUT /api/operation
    /// DomainObject response = _client.PutAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> PutAsync<T>(string operation, CancellationToken cancel = default);
    #endregion

    #region Delete<HttpResponseMessage>
    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true (passing also given headers and object as content data)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     requestObject,
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and content data object.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12 (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL, query parameters and content object data.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation?audit=true (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     new QueryParameters("audit", true),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and content object data.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and query parameters.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation?audit=true
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL.<br/>
    /// Returns Raw HttpResponseMessage as received from service.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<HttpResponseMessage> DeleteAsync(string operation, CancellationToken cancel = default);
    #endregion

    #region Delete<T>
    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and optional:<br/>
    ///   - query parameters<br/>
    ///   - ad-hoc header(s) added to request<br/>
    ///   - request content data (request object).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true (passing also given headers and object as content data)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     requestObject,
    ///     new Dictionary&lt;string, string&gt; {{ "Accept", "string/xml" }});
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="headers">Additional request header(s) to add to this request (in addition to default global headers, which are defined in settings).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, PathParameters? pathParameters, QueryParameters? queryParameters, object? data, Dictionary<string, string>? headers, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and:<br/>
    ///   - query parameters<br/>
    ///   - request content data (request object).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and content data object.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12 (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL, query parameters and content object data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation?audit=true (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation",
    ///     new QueryParameters("audit", true),
    ///     requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and content object data.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation (passing also content data object)
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", requestObject);
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="data">The data object to be sent as request payload. Object gets serialized internally.</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, object data, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts) and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12?audit=true
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;(
    ///     "/api/operation/{id}",
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain interpolated {value} parts).<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation/12
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation/{id}", new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/> parameter.</param>
    /// <param name="pathParameters">A PathParameters (Expando) object of parameters to fill the <paramref name="operation"/> interpolation parts with given values ("api/codes/{id}" + new { id = 12 }).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, PathParameters pathParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL and query parameters.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation?audit=true
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation", new QueryParameters("audit", true));
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, QueryParameters queryParameters, CancellationToken cancel = default);

    /// <summary>
    /// Performs Asynchronous HTTP DELETE operation with specified operation URL.<br/>
    /// Returns deserialized typed object of &lt;T&gt;.<br/>
    /// May return null for nullable objects if response status is <see cref="HttpStatusCode.NoContent"/>.<br/>
    /// <code>
    /// // Makes an API call to DELETE /api/operation
    /// DomainObject result = _client.DeleteAsync&lt;DomainObject&gt;("/api/operation");
    /// </code>
    /// </summary>
    /// <exception cref="RestClientException">Thrown if request or serialization/deserialization failed. Custom exception properties and Exception.Data contains details on failure.</exception>
    /// <param name="operation">API endpoint address for resource (without base path).</param>
    /// <param name="cancel">Optional: Asynchronous operation cancellation token.</param>
    Task<T?> DeleteAsync<T>(string operation, CancellationToken cancel = default);
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
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("filter", "yes"),
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
        PathParameters? pathParameters,
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
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("filter", "yes"));
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
        PathParameters pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new PathParameters("id", 12));
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
        PathParameters pathParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car?filter=yes" with "Car" object as content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car",
    ///     new Car { Id = 69, Name = "Aston Martin" },
    ///     new QueryParameters("filter", "yes"));
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
    ///     new PathParameters("id", 12),
    ///     new QueryParameters("filter", "yes"));
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">Keys:Values to be replaced in interpolated <paramref name="operation"/>.</param>
    /// <param name="queryParameters">Additional parameters to be added in request query (operation?qp1=val&amp;qp2=val).</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        PathParameters pathParameters,
        QueryParameters queryParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car/12" without setting content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car/{id}",
    ///     new PathParameters("id", 12));
    /// </code>
    /// </summary>
    /// <param name="method">Http Method to use (GET, PUT, etc.)</param>
    /// <param name="operation">API endpoint address for resource (without base path). Can be interpolated string, if used with <paramref name="pathParameters"/>.</param>
    /// <param name="pathParameters">Keys:Values to be replaced in interpolated <paramref name="operation"/>.</param>
    Task<HttpRequestMessage> GetRequestMessage(
        HttpMethod method,
        string operation,
        PathParameters pathParameters);

    /// <summary>
    /// Composes and returns raw <see cref="HttpRequestMessage"/> with given parameters and authentication from setup. Can be called with _api.SendHttpRequest(request) method.
    /// <code>
    /// // Creates request PUT "/api/car?filter=yes" without setting content.
    /// var request = await _api.GetRequestMessage(
    ///     HttpMethod.Put,
    ///     "/api/car",
    ///     new QueryParameters("filter", "yes"));
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
