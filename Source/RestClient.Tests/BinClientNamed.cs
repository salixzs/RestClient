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
    public class BinClientNamed : AbstractNamedRestClient
    {
        public BinClientNamed(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger) : base(httpClientFactory, settings, logger)
        {
        }

        protected override string ClientName { get; } = "bin";

        protected override (string Key, string Value) GetAuthenticationKeyValue() => new("Bearer", "123123123123");
    }
}
