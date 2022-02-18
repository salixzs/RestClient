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
    public class IntegrationTestsTyped
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;

        public IntegrationTestsTyped(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

        [Fact]
        public async Task Get_Guid_Succeeds()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var result = await _api.GetAsync<Uuid>("uuid");
            result.Should().NotBeNull();
            result.uuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Get_TwoCalls_Succeeds()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var result1 = await _api.GetAsync<Uuid>("uuid");
            var result2 = await _api.GetAsync<MyIp>("ip");
            result1.Should().NotBeNull();
            result1.uuid.Should().NotBeEmpty();
            result2.Should().NotBeNull();
            result2.origin.Should().NotBeEmpty().And.Contain(".");
        }

        [Fact]
        public async Task Auth_BasicCorrect_Succeeds()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Basic, UserName = "me", Password = "secret" } }, _logger);
            var result = await _api.GetAsync("basic-auth/{user}/{password}", new { user = "me", password = "secret" });
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Auth_BasicWrong_Unauthorized()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Basic, UserName = "me", Password = "secret" } }, _logger);
            try
            {
                var result = await _api.GetAsync("basic-auth/{user}/{password}", new { user = "notme", password = "hola" });
                result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
            catch (RestClientException ex)
            {
                ex.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }
        }

        [Fact]
        public async Task Auth_Bearer_Succeeds()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.External } }, _logger);
            var result = await _api.GetAsync("bearer");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await result.Content.ReadAsStringAsync();
            response.Should().Contain("123123123123");
        }
    }
}
