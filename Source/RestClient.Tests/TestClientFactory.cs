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
    public class TestClientFactory : AbstractRestClient
    {
        public TestClientFactory(IHttpClientFactory httpClientFactory, RestServiceSettings parameters, ILogger logger)
            : base(httpClientFactory, parameters, logger, NewtonsoftJsonObjectSerializer.Default)
        {
        }
    }
}
