using System;
using System.Collections.Generic;
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
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;

        public IntegrationTests(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

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
            var result = await _api.GetAsync("basic-auth/{user}/{password}", new PathParameters("user", "me", "password", "secret"));
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Auth_BasicWrong_Unauthorized()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org", Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.Basic, UserName = "me", Password = "secret" } }, _logger);
            try
            {
                var result = await _api.GetAsync("basic-auth/{user}/{password}", new PathParameters("user", "notme", "password", "hola"));
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

        [Fact]
        public async Task Get_DefaultHeaders_Succeeds()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org", RequestHeaders = new Dictionary<string, string> { { "Uno", "Momento" } } }, _logger);
            var result = await _api.GetAsync<MethodResponse>("get");
            result.Should().NotBeNull();
            result.headers.Should().NotBeEmpty();
            result.headers.Should().ContainKey("Uno");
            result.headers["Uno"].Should().Be("Momento");

            // Doing another call to see setting headers twice does not happen
            var result2 = await _api.GetAsync<MethodResponse>("get", null, null, null, new Dictionary<string, string> { { "Req", "Yepp" } });
            result2.Should().NotBeNull();
            result2.headers.Should().NotBeEmpty();
            result2.headers.Should().ContainKey("Uno");
            result2.headers["Uno"].Should().Be("Momento");
            result2.headers.Should().ContainKey("Req");
            result2.headers["Req"].Should().Be("Yepp");
        }

        [Fact]
        public async Task Get_WrongEndpoint_Throws()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            try
            {
                var result = await _api.GetAsync<Uuid>("guid");
                result.Should().BeNull();
            }
            catch (RestClientException ex)
            {
                ex.Message.Should().Contain("Error occurred in API/Service.");
                ex.ReasonPhrase.Should().Be("NOT FOUND");
                ex.Data.Should().NotBeNull();
                ex.Data.Count.Should().Be(4);
                ex.Data["Api.Uri"].Should().Be($"https://httpbin.org/guid");
                ex.Data["Api.StatusCode"].Should().Be(HttpStatusCode.NotFound);
                ex.Data["Api.Method"].Should().Be("GET");
                ex.StatusCode.Should().Be(HttpStatusCode.NotFound);
                ex.Method.Should().Be(HttpMethod.Get);
            }
        }

        [Fact]
        public async Task Get_WrongObject_Throws()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            try
            {
                var result = await _api.GetAsync<Guid?>("uuid");
                result.Should().BeNull();
            }
            catch (RestClientException ex)
            {
                ex.Message.Should().Contain("Error occurred while deserializing API response");
                ex.Data.Should().NotBeNull();
                ex.Data.Count.Should().BeGreaterOrEqualTo(2);
                ex.Data["Api.Uri"].Should().Be($"https://httpbin.org/uuid");
                ex.Data["Api.Method"].Should().Be("GET");
                ex.InnerException.Should().NotBeNull();
                ex.InnerException.Should().BeOfType(typeof(Newtonsoft.Json.JsonSerializationException));
                ex.InnerException.Message.Should().Contain("Cannot deserialize");
            }
        }
    }
}
