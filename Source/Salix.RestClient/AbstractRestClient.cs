using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient
{
    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class AbstractRestClient : IAbstractRestClient
    {
        private readonly IObjectSerializer _serializer;
        private readonly RestServiceSettings _settings;
        private readonly ILogger _logger;
        protected IHttpClientFactory HttpClientFactory { get; set; }

        /// <summary>
        /// Last operation timing.
        /// </summary>
        public TimeSpan CallTime { get; private set; }

        /// <summary>
        /// Generic, typed REST (API) Service client instance.
        /// </summary>
        protected AbstractRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger) : this(httpClientFactory, settings, logger, null)
        {

        }

        /// <summary>
        /// Generic, typed REST (API) Service client instance.
        /// </summary>
        protected AbstractRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger, IObjectSerializer serializer)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger;
            _serializer = serializer ?? NewtonsoftJsonObjectSerializer.Default;
            _logger.LogDebug($"Created API RestClient to {settings.ServiceUrl}");
        }

        #region HTTP Method wrappers
        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data, Dictionary<string, string> headers)
            => await this.SendHttpRequest(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data)
            => await this.GetAsync(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="data">The data to be sent as request payload.</param>
        public virtual async Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, object data)
            => await this.GetAsync(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve nonJSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="data">The data to be sent as request payload.</param>
        public virtual async Task<HttpResponseMessage> GetAsync(string operation, object data)
            => await this.GetAsync(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        public virtual async Task<HttpResponseMessage> GetAsync(string operation)
            => await this.GetAsync(operation, null, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data, Dictionary<string, string> headers)
            => await this.SendHttpRequest<T>(HttpMethod.Get, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data)
            => await this.GetAsync<T>(operation, pathParameters, queryParameters, data, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.GetAsync<T>(operation, pathParameters, queryParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> GetAsync<T>(string operation, dynamic pathParameters = null)
            => await this.GetAsync<T>(operation, pathParameters, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        public virtual async Task<T> GetAsync<T>(string operation)
            => await this.GetAsync<T>(operation, null, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers)
            => await this.SendHttpRequest(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.PostAsync(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters)
            => await this.PostAsync(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        public virtual async Task<HttpResponseMessage> PostAsync(string operation, object data)
            => await this.PostAsync(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Usually used to perform data inserts or updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers)
            => await this.SendHttpRequest<T>(HttpMethod.Post, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Usually used to perform data inserts or updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.PostAsync<T>(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Usually used to perform data inserts or updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters)
            => await this.PostAsync<T>(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Usually used to perform data inserts or updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        public virtual async Task<T> PostAsync<T>(string operation, object data)
            => await this.PostAsync<T>(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers)
            => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.PutAsync<T>(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters)
            => await this.PutAsync<T>(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        public virtual async Task<T> PutAsync<T>(string operation, object data)
            => await this.PutAsync<T>(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers)
            => await this.SendHttpRequest<T>(HttpMethod.Put, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.PatchAsync<T>(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters)
            => await this.PatchAsync<T>(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> PatchAsync<T>(string operation, object data)
            => await this.PatchAsync<T>(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers)
            => await this.SendHttpRequest<T>(HttpMethod.Delete, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters)
            => await this.DeleteAsync<T>(operation, data, pathParameters, queryParameters, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters)
            => await this.DeleteAsync<T>(operation, data, pathParameters, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        public virtual async Task<T> DeleteAsync<T>(string operation, object data)
            => await this.DeleteAsync<T>(operation, data, null, null, null).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        public virtual async Task DeleteAsync(string operation, object data) => await this.SendHttpRequest(HttpMethod.Delete, operation, data).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        public virtual async Task DeleteAsync(string operation, object data, dynamic pathParameters) =>
            await this.SendHttpRequest(HttpMethod.Delete, operation, data, pathParameters).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        public virtual async Task DeleteAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters) =>
            await this.SendHttpRequest(HttpMethod.Delete, operation, data, pathParameters, queryParameters).ConfigureAwait(false);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        public virtual async Task DeleteAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers) =>
            await this.SendHttpRequest(HttpMethod.Delete, operation, data, pathParameters, queryParameters, headers).ConfigureAwait(false);
        #endregion

        /// <summary>
        /// Sends the HTTP request based on given criteria (parameters).
        /// </summary>
        /// <param name="method">The HTTP method to be used for request.</param>
        /// <param name="operation">The operation URI - resource to be used.</param>
        /// <param name="data">The data to be sent as request payload.</param>
        /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
        /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        private async Task<HttpResponseMessage> SendHttpRequest(HttpMethod method, string operation, object data = null, dynamic pathParameters = null, QueryParameterCollection queryParameters = null, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result;
            var timer = Stopwatch.StartNew();
            using (HttpRequestMessage request = this.CreateRequest(method, operation, pathParameters, queryParameters))
            {
                if (data != null)
                {
                    _logger.LogTrace("Adding payload data to API RestClient request.");
                    request.Content = new StringContent(await _serializer.SerializeAsync(data), Encoding.UTF8, "application/json");
                }

                // Handling BASIC authentication
                if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.Basic)
                {
                    _logger.LogTrace($"Adding Basic authentication token.");
                    var usernamePassword = Encoding.ASCII.GetBytes($"{_settings.Authentication.UserName}:{_settings.Authentication.Password}");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(usernamePassword));
                }

                // Handling BEARER authentication
                if (_settings.Authentication.AuthenticationType == ApiAuthenticationType.Bearer)
                {
                    _logger.LogTrace($"Adding Bearer token.");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.Authentication.BearerToken);
                }

                // Demand JSON by default, if not specified otherwise
                if (!_settings.RequestHeaders.ContainsKey("Accept"))
                {
                    _logger.LogTrace($"Adding default Accept header for JSON.");
                    request.Headers.Add("Accept", "application/json");
                }

                foreach (var requestHeader in _settings.RequestHeaders)
                {
                    _logger.LogTrace($"Adding request header {requestHeader.Key} to API RestClient request (from settings).");
                    request.Headers.Add(requestHeader.Key, requestHeader.Value);
                }

                if (headers != null && headers.Count > 0)
                {
                    foreach (var hdr in headers)
                    {
                        if (request.Headers.Contains(hdr.Key))
                        {
                            _logger.LogTrace($"Changing value of request header {hdr.Key} in API RestClient request.");
                            request.Headers.Remove(hdr.Key);
                            request.Headers.Add(hdr.Key, hdr.Value);
                        }
                        else
                        {
                            _logger.LogTrace($"Adding request header {hdr.Key} to API RestClient request.");
                            request.Headers.Add(hdr.Key, hdr.Value);
                        }
                    }
                }

                // CALLING THE SERVICE HERE!!!
                using (var client = this.HttpClientFactory.CreateClient())
                {
                    client.BaseAddress = new Uri(_settings.ServiceUrl);
                    _logger.LogDebug($"Calling API {client.BaseAddress} {method} {operation}");
                    result = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                }
            }

            if (!result.IsSuccessStatusCode)
            {
                _logger.LogTrace($"API call failed with status code {result.StatusCode}");
                var contentString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                RestClientException retException;
                var requestUri = result.RequestMessage.RequestUri?.AbsoluteUri;
                var errMsg =
                    "Error occurred in API/Service.\n"
                    + $"Request status code: {(int)result.StatusCode} ({result.StatusCode}).\n"
                    + $"{result.RequestMessage.Method.Method} {requestUri}";
                retException = new RestClientException(errMsg);
                retException.Data.Add("Api.Uri", requestUri);

                retException.Data.Add("Api.StatusCode", result.StatusCode);
                retException.StatusCode = result.StatusCode;
                retException.Data.Add("Api.RawError", contentString);
                retException.Data.Add("Api.Method", result.RequestMessage.Method.Method);
                retException.Method = result.RequestMessage.Method;
                retException.ResponseContent = contentString;

                this.StopTimer(timer);
                throw retException;
            }

            this.StopTimer(timer);
            return result;
        }

        /// <summary>
        /// Sends the HTTP request based on given criteria (parameters).
        /// </summary>
        /// <typeparam name="T">Expected return type (if any).</typeparam>
        /// <param name="method">The HTTP method to be used for request.</param>
        /// <param name="operation">The operation URI - resource to be used.</param>
        /// <param name="data">The data to be sent as request payload.</param>
        /// <param name="pathParameters">The path parameters, replaced placeholders in operation URI (ex. /resource/{id}).</param>
        /// <param name="queryParameters">The query parameters to be added to operation URI after ? mark.</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        private async Task<T> SendHttpRequest<T>(HttpMethod method, string operation, object data = null, dynamic pathParameters = null, QueryParameterCollection queryParameters = null, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = await this.SendHttpRequest(method, operation, data, pathParameters, queryParameters, headers);

            // 204 = generally success code, but no results
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                _logger.LogTrace("API call returned empty result");

                // Returns null for classes or nullable value types (and strings), otherwise default value type
                // Lists if explicitly set as type parameter get initialized as empty lists
                return default;
            }

            var contentString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var returnValue = await _serializer.DeserializeAsync<T>(contentString);
                return returnValue;
            }
            catch (Exception ex)
            {
                var requestUri = result.RequestMessage?.RequestUri?.AbsoluteUri ?? "---";
                var requestMethod = result.RequestMessage?.Method?.Method ?? "---";
                var errMsg =
                    $"Error occurred while deserializing API response to {typeof(T).FullName}.\n"
                    + "Make sure you are calling correct operation and deserializing result to correct type.\n"
                    + $"Request status code: {(int)result.StatusCode} ({result.StatusCode}).\n"
                    + $"{requestMethod} {requestUri}";
                var retException = new RestClientException(errMsg, ex);
                retException.Data.Add("Api.Uri", requestUri);
                retException.Data.Add("Api.Method", requestMethod);
                throw retException;
            }
        }

        /// <summary>
        /// Creates the HTTP request for API client to execute.
        /// </summary>
        /// <param name="method">The HTTP method to use for request.</param>
        /// <param name="operation">The operation (base URL).</param>
        /// <param name="pathParameters">The path parameters (replacing placeholders in base path).</param>
        /// <param name="queryParameters">The query parameters - added after question mark in URL.</param>
        /// <returns>
        /// A formed API Request to execute through HttpClient.
        /// </returns>
        protected virtual HttpRequestMessage CreateRequest(HttpMethod method, string operation, dynamic pathParameters, QueryParameterCollection queryParameters)
            => new(method, this.ComposeFullOperationUrl(operation, pathParameters, queryParameters));

        /// <summary>
        /// Composes the full operation URL with base URL, operation and optional query parameters.
        /// </summary>
        /// <param name="operation">The operation of API (like "api/data").</param>
        /// <param name="pathParameters">The path parameters.</param>
        /// <param name="queryParameters">The query parameters.</param>
        private string ComposeFullOperationUrl(string operation, dynamic pathParameters, QueryParameterCollection queryParameters)
        {
            // Replace all path parameters
            if (pathParameters != null)
            {
                foreach (PropertyInfo property in pathParameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    _logger.LogTrace($"API URL: Replacing {property.Name} with value {property.GetValue(pathParameters, null).ToString()}");
                    operation = operation.Replace($"{{{property.Name}}}", property.GetValue(pathParameters, null).ToString());
                }
            }

            // Add query parameters
            if (queryParameters != null)
            {
                _logger.LogTrace($"API URL: Adding query parameters: {queryParameters}");
                operation = $"{operation}?{queryParameters}";
            }

            return operation;
        }

        /// <summary>
        /// Common things to finalize operation timing.
        /// </summary>
        private void StopTimer(Stopwatch timer)
        {
            timer.Stop();
            _logger.LogDebug($"API call took {timer.Elapsed}");
            this.CallTime = timer.Elapsed;
        }

        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay => $"RestClient to {_settings.ServiceUrl}";
    }
}
