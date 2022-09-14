using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
                new PathParameters("id", 12),
                new QueryParameters { { "filter", "yes" } },
                new Dictionary<string, string> { { "Duo", "Symmetry" } });

            sut.Should().NotBeNull();
            sut.Method.Should().Be(HttpMethod.Put);
            sut.RequestUri.OriginalString.Should().Be("/api/car/12?filter=yes");

            var serializedContent = await sut.Content.ReadAsStringAsync(CancellationToken.None);
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

        [Fact]
        public async Task GetRequest_StringContent()
        {
            var sut = await _api.GetRequestMessage(
                HttpMethod.Post,
                "/api/car/{id}",
                new StringContent("abracadabra"),
                new PathParameters("id", 21));

            sut.Should().NotBeNull();

            sut.Content.Should().BeOfType(typeof(StringContent));
            var serializedContent = await sut.Content.ReadAsStringAsync(CancellationToken.None);
            serializedContent.Should().NotBeNullOrEmpty();
            serializedContent.Should().Be("abracadabra");
        }

        [Fact]
        public async Task GetRequest_FormMultipart()
        {
            HttpRequestMessage sut;
            var filename = string.Empty;
            var testContent = string.Empty;
            using (var content = new MultipartFormDataContent())
            {
                var byteArray = Encoding.UTF8.GetBytes("banzai");
                StreamContent fileContent;
                using (var stream = new MemoryStream(byteArray))
                {
                    fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                    content.Add(
                        content: fileContent,
                        name: "\"files\"",
                        fileName: "testing.txt");

                    sut = await _api.GetRequestMessage(
                        HttpMethod.Post,
                        "/api/car/{id}",
                        content,
                        new PathParameters("id", 10033));

                    sut.Should().NotBeNull();
                    sut.Content.Should().BeOfType(typeof(MultipartFormDataContent));
                    var dataContents = sut.Content as MultipartFormDataContent;
                    foreach (var dataContent in dataContents)
                    {
                        filename = dataContent.Headers.ContentDisposition.FileName;
                        testContent = await dataContent.ReadAsStringAsync();
                    }
                }
            }

            filename.Should().Be("testing.txt");
            testContent.Should().Be("banzai");
        }
    }
}
