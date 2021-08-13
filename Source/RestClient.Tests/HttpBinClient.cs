using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Tests
{
    /// <summary>
    /// Test client to https://httpbin.org for integration testing (actual calls).
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HttpBinClient : AbstractRestClient
    {
        public HttpBinClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger) : base(httpClientFactory, settings, logger)
        {
        }
    }
}