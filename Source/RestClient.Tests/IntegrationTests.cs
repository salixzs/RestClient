using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Salix.RestClient;
using Xunit;
using Xunit.Abstractions;
namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        private readonly HttpClient _httpClient;
        private readonly XUnitLogger<HttpBinClient> _logger;
        private HttpBinClient _api;

        public IntegrationTests(ITestOutputHelper output)
        {
            _httpClient = new HttpClient();
            _logger = new XUnitLogger<HttpBinClient>(output);
        }

        [Fact]
        public async Task Get_Guid_Succeeds()
        {
            _api = new HttpBinClient(_httpClient, new RestServiceSettings { ServiceUrl = "https://httpbin.org" }, _logger);
            var result = await _api.GetAsync<GuidHolder>("uuid");
            result.Should().NotBeNull();
            result.uuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Auth_BasicCorrect_Succeeds()
        {
            _api = new HttpBinClient(_httpClient, new RestServiceSettings { ServiceUrl = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Basic, UserName = "me", Password = "secret" } }, _logger);
            var result = await _api.GetAsync("basic-auth/{user}/{password}", new { user = "me", password = "secret" });
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Auth_BasicWrong_Unauthorized()
        {
            _api = new HttpBinClient(_httpClient, new RestServiceSettings { ServiceUrl = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Basic, UserName = "me", Password = "secret" } }, _logger);
            try
            {
                var result = await _api.GetAsync("basic-auth/{user}/{password}", new { user = "notme", password = "hola" });
                result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
            catch(RestClientException ex)
            {
                ex.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
        }

        [Fact]
        public async Task Auth_Bearer_Succeeds()
        {
            _api = new HttpBinClient(_httpClient, new RestServiceSettings { ServiceUrl = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Bearer, BearerToken = "123123123123" } }, _logger);
            var result = await _api.GetAsync("bearer");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await result.Content.ReadAsStringAsync();
            response.Should().Contain("123123123123");
        }
    }

    public class GuidHolder
    {
        public Guid uuid { get; set; }
    }
}
