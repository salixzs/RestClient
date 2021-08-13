using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Salix.RestClient
{
    public interface IAbstractRestClient
    {
        /// <summary>
        /// Last operation timing.
        /// </summary>
        TimeSpan CallTime { get; }

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        Task<T> GetAsync<T>(string operation);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<T> GetAsync<T>(string operation, dynamic pathParameters = null);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Usually used to perform data retrievals from API services.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data);

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
        Task<T> GetAsync<T>(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        Task<HttpResponseMessage> GetAsync(string operation);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve nonJSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="data">The data to be sent as request payload.</param>
        Task<HttpResponseMessage> GetAsync(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="data">The data to be sent as request payload.</param>
        Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, object data);

        /// <summary>
        /// Performs Asynchronous HTTP GET operation with specified operation URL (may contain some business ID(s)) and optional query parameters.
        /// Returns Raw HttpResponseMessage. Can be used with XML SOAP services to be able to retrieve non-JSON data.
        /// </summary>
        /// <typeparam name="T">Type of data to retrieve.</typeparam>
        /// <param name="operation">The operation URL.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="data">The data to be sent as request payload.</param>
        Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data);

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
        Task<HttpResponseMessage> GetAsync(string operation, dynamic pathParameters, QueryParameterCollection queryParameters, object data, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task<T> PostAsync<T>(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

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
        Task<T> PostAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task<HttpResponseMessage> PostAsync(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

        /// <summary>
        /// Performs Asynchronous HTTP POST operation with specified operation URL and data.
        /// Returns Raw HttpResponseMessage to handle all in caller side. Can be used to call SOPA services (XML data serialization).
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        Task<HttpResponseMessage> PostAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task<T> PatchAsync<T>(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP PATCH operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services. Note: Uses PUT method under curtains.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

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
        Task<T> PatchAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task<T> PutAsync<T>(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP PUT operation with specified operation URL using specified method and passing data.
        /// Usually used to perform data updates through API services.
        /// </summary>
        /// <typeparam name="T">Type of data that will be retrieved.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

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
        Task<T> PutAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task DeleteAsync(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task DeleteAsync(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task DeleteAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        /// <param name="headers">Additional request header(s) to add to this request in addition to default global headers (added in client setup).</param>
        Task DeleteAsync(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        Task<T> DeleteAsync<T>(string operation, object data);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters);

        /// <summary>
        /// Performs Asynchronous HTTP DELETE operation with specified operation URL (may contain some business ID(s)) and optional query parameters
        /// Usually used to perform data removal from API services.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="operation">The operation URL. Use <see cref="Dto.Endpoints"/> namespace definitions for this. May contain IDs of business objects.</param>
        /// <param name="data">Data object that should be sent to server.</param>
        /// <param name="pathParameters">A dynamic (Expando) object of parameters to fill the operation path with (paths like "api/codes/{id}").</param>
        /// <param name="queryParameters">The list of parameters to be added to operation (in Query string, like ...operation?param1=val1&amp;param2=val2).</param>
        Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters);

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
        Task<T> DeleteAsync<T>(string operation, object data, dynamic pathParameters, QueryParameterCollection queryParameters, Dictionary<string, string> headers);
    }
}
