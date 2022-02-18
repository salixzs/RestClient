using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestHttpClientFactory : IHttpClientFactory
    {
        private readonly string _name;

        public TestHttpClientFactory()
        {
        }

        public TestHttpClientFactory(string name) => _name = name;

        public HttpClient CreateClient(string name) => new();
    }
}
