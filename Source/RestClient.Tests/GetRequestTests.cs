using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Salix.RestClient;
using Xunit;
using Xunit.Abstractions;

namespace RestClient.Tests
{
    public class GetRequestTests
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;

        public GetRequestTests(ITestOutputHelper output)
        {
            _logger = new XUnitLogger<BinClientTyped>(output);
            _api = new BinClientTyped(
                _httpClient,
                new RestServiceSettings
                {
                    BaseAddress = "https://httpbin.org",
                    Authentication = new RestServiceAuthentication
                    {
                        AuthenticationType = ApiAuthenticationType.Basic,
                        UserName = "me",
                        Password = "secret"
                    },
                    RequestHeaders = new Dictionary<string, string>
                    {
                        { "Uno", "Momento" }
                    }
                },
                _logger);
        }

        [Fact]
        public async Task GetRequest_All()
        {
            var sut = await _api.GetRequestMessage(
                HttpMethod.Put,
                "/api/car/{id}",
                new RequestObject { Id = 69, Name = "Aston Martin" },
                new { id = 12 },
                new QueryParameters { { "filter", "yes" } },
                new Dictionary<string, string> { { "Duo", "Symmetry" } });

            sut.Should().NotBeNull();
            sut.Method.Should().Be(HttpMethod.Put);
            sut.RequestUri.OriginalString.Should().Be("/api/car/12?filter=yes");

            var serializedContent = sut.Content.ReadAsStringAsync(CancellationToken.None).Result;
            serializedContent.Should().NotBeNullOrEmpty();
            serializedContent.Should().Be("{\"Id\":69,\"Name\":\"Aston Martin\"}");

            sut.Headers.Should().HaveCountGreaterOrEqualTo(2);
            sut.Headers.Accept.Should().HaveCount(1);
            sut.Headers.Accept.First().MediaType.Should().Be("application/json");

            sut.Headers.Should().ContainKey("Duo");
            var requestHeader =
                sut.Headers.FirstOrDefault(h => h.Key == "Duo");
            requestHeader.Should().NotBeNull();
            requestHeader.Value.FirstOrDefault().Should().NotBeNull();
            requestHeader.Value.FirstOrDefault().Should().Be("Symmetry");

            var auth = sut.Headers.Authorization;
            auth.Should().NotBeNull();
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes("me:secret"));
            auth.Scheme.Should().Be("Basic");
            auth.Parameter.Should().Be(token);
        }

    }
}
