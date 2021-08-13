using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new();
    }
}
