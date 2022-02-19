using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Salix.RestClient;
using Xunit;
using Xunit.Abstractions;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationGet
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;
        private const string BaseAddress = "https://httpbin.org";

        public IntegrationGet(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

        [Fact]
        public async Task Get_Empty()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("get");
            result.Should().NotBeNull();

            result.args.Should().BeEmpty();
            result.headers.Should().HaveCountGreaterOrEqualTo(1);
            result.headers.Should().ContainKey("Accept");
            result.headers["Accept"].Should().Be("application/json");
            result.url.Should().Be($"{BaseAddress}/get");
            result.origin.Should().NotBeEmpty();
            result.origin.Should().Contain(".");
        }

        [Fact]
        public async Task Get_QueryArgs()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("get", new QueryParameterCollection { { "skip", 5 }, { "take", 25 } });
            result.Should().NotBeNull();
            result.args.Should().NotBeEmpty();
            result.args.Should().HaveCount(2);
            result.url.Should().Be($"{BaseAddress}/get?skip=5&take=25");
        }

        [Fact]
        public async Task Get_QueryPath()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("{method}", new { method = "get" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/get");
        }

        [Fact]
        public async Task Get_QueryPathArgs()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("{method}", new { method = "get" }, new QueryParameterCollection { { "skip", 5 }, { "take", 25 } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/get?skip=5&take=25");
        }

        [Fact]
        public async Task Get_QueryData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("anything", null, null, new RequestObject { Id = 12, Name = "Test" }, null);
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/anything");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Get_QueryPathArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("{method}", new { method = "anything" }, new QueryParameterCollection { { "skip", 5 }, { "take", 25 } }, new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/anything?skip=5&take=25");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Get_PathData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("{method}", new { method = "anything" }, new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/anything");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Get_QueryArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("anything", new QueryParameterCollection { { "skip", 5 }, { "take", 25 } }, new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/anything?skip=5&take=25");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Get_QueryHeaders()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.GetAsync<MethodResponse>("{method}", new { method = "get" }, null, null, new Dictionary<string, string> { { "Numero", "Uno" } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/get");
            result.headers.Should().HaveCountGreaterOrEqualTo(1);
            result.headers.Should().ContainKey("Numero");
            result.headers["Numero"].Should().Be("Uno");
        }
    }
}
