using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Tests
{
    /// <summary>
    /// RestClient implementation for testing needs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TestClient : AbstractRestClient
    {
        /// <summary>
        /// RestClient implementation for testing needs.
        /// </summary>
        /// <param name="httpClient">The HTTP client itself.</param>
        /// <param name="parameters">The parameters for client instance.</param>
        /// <param name="logger">The object for logging.</param>
        public TestClient(IHttpClientFactory httpClientFactory, RestServiceSettings parameters, ILogger logger)
            : base(httpClientFactory, parameters, logger, NewtonsoftJsonObjectSerializer.Default)
        {
        }

        ///// <summary>
        ///// Exposes internal HttpClient instance.
        ///// </summary>
        //// public HttpClient Client => this.HttpClient;

        ///// <summary>
        ///// Stored last called URL for request,
        ///// if overridden method GetAsync{T} with 3 parameters are called (see below in this class).
        ///// </summary>
        //public Uri LastCalledUrl { get; private set; }

        ///// <summary>
        ///// Overridden method for GET with all query and dynamic parameters to
        ///// test whether URL is composed as expected.
        ///// Stores it in LastCalledUrl property.
        ///// </summary>
        ///// <typeparam name="T">Type of object to get.</typeparam>
        ///// <param name="operation">The operation - base URL.</param>
        ///// <param name="pathParameters">The path parameters.</param>
        ///// <param name="queryParameters">The query parameters.</param>
        ///// <returns>Nothing.</returns>
        //public override Task<T> GetAsync<T>(
        //    string operation,
        //    dynamic pathParameters,
        //    QueryParameterCollection queryParameters)
        //{
        //    using (HttpRequestMessage request =
        //        this.CreateRequest(HttpMethod.Get, operation, pathParameters, queryParameters))
        //    {
        //        this.LastCalledUrl = request.RequestUri;
        //    }

        //    return Task.FromResult<T>(default);
        //}
    }
}
