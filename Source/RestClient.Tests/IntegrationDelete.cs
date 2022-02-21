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
    public class IntegrationDelete
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;
        private const string BaseAddress = "https://httpbin.org";

        public IntegrationDelete(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

        [Fact]
        public async Task Delete_Empty()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("delete");
            result.Should().NotBeNull();

            result.args.Should().BeEmpty();
            result.headers.Should().HaveCountGreaterOrEqualTo(1);
            result.headers.Should().ContainKey("Accept");
            result.headers["Accept"].Should().Be("application/json");
            result.url.Should().Be($"{BaseAddress}/delete");
            result.origin.Should().NotBeEmpty();
            result.origin.Should().Contain(".");
        }

        [Fact]
        public async Task Delete_QueryArgs()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("delete", new QueryParameters { { "skip", 5 }, { "take", 25 } });
            result.Should().NotBeNull();
            result.args.Should().NotBeEmpty();
            result.args.Should().HaveCount(2);
            result.url.Should().Be($"{BaseAddress}/delete?skip=5&take=25");
        }

        [Fact]
        public async Task Delete_QueryPath()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("{method}", new PathParameters("method", "delete"));
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete");
        }

        [Fact]
        public async Task Delete_QueryPathArgs()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("{method}", new PathParameters("method", "delete"), new QueryParameters { { "skip", 5 }, { "take", 25 } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete?skip=5&take=25");
        }

        [Fact]
        public async Task Delete_Data()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("delete", new RequestObject { Id = 12, Name = "Test" }, null, null, null);
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Delete_QueryPathArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("{method}", new PathParameters("method", "delete"), new QueryParameters { { "skip", 5 }, { "take", 25 } }, new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete?skip=5&take=25");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Delete_PathData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("{method}", new PathParameters("method", "delete"), new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Delete_QueryArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("delete", new QueryParameters { { "skip", 5 }, { "take", 25 } }, new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete?skip=5&take=25");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }

        [Fact]
        public async Task Delete_QueryHeaders()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.DeleteAsync<MethodResponse>("delete", null, null, null, new Dictionary<string, string> { { "Numero", "Uno" } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/delete");
            result.headers.Should().HaveCountGreaterOrEqualTo(1);
            result.headers.Should().ContainKey("Numero");
            result.headers["Numero"].Should().Be("Uno");
        }
    }
}
