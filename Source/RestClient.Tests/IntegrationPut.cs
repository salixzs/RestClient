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
    public class IntegrationPut
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTyped _api;
        private const string BaseAddress = "https://httpbin.org";

        public IntegrationPut(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

        [Fact]
        public async Task Put_Empty()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.PutAsync<MethodResponse>("put");
            result.Should().NotBeNull();

            result.args.Should().BeEmpty();
            result.headers.Should().HaveCountGreaterOrEqualTo(1);
            result.headers.Should().ContainKey("Accept");
            result.headers["Accept"].Should().Be("application/json");
            result.url.Should().Be($"{BaseAddress}/put");
            result.origin.Should().NotBeEmpty();
            result.origin.Should().Contain(".");
        }

        [Fact]
        public async Task Put_Data()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.PutAsync<MethodResponse>("put", new RequestObject { Id = 12, Name = "Test" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/put");
            AssertData(result);
        }

        [Fact]
        public async Task Put_ArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.PutAsync<MethodResponse>("put", new RequestObject { Id = 12, Name = "Test" }, new QueryParameterCollection { { "audit", true } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/put?audit=True");
            AssertData(result);
        }

        [Fact]
        public async Task Put_PathData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.PutAsync<MethodResponse>("{method}", new RequestObject { Id = 12, Name = "Test" }, new { method = "put" });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/put");
            AssertData(result);
        }

        [Fact]
        public async Task Put_PathArgsData()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress }, _logger);
            var result = await _api.PutAsync<MethodResponse>("{method}", new RequestObject { Id = 12, Name = "Test" }, new { method = "put" }, new QueryParameterCollection { { "audit", true } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/put?audit=True");
            AssertData(result);
        }

        [Fact]
        public async Task Put_Headers()
        {
            _api = new BinClientTyped(_httpClient, new RestServiceSettings { BaseAddress = BaseAddress, RequestHeaders = new Dictionary<string, string> { { "Global", "head" } } }, _logger);
            var result = await _api.PutAsync<MethodResponse>("put", new RequestObject { Id = 12, Name = "Test" }, null, null, new Dictionary<string, string> { { "Per-Request", "testing" } });
            result.Should().NotBeNull();
            result.url.Should().Be($"{BaseAddress}/put");
            AssertData(result);
            result.headers.Should().HaveCountGreaterOrEqualTo(2);
            result.headers.Should().ContainKey("Global");
            result.headers["Global"].Should().Be("head");
            result.headers.Should().ContainKey("Per-Request");
            result.headers["Per-Request"].Should().Be("testing");
        }

        private static void AssertData(MethodResponse result)
        {
            result.data.Should().NotBeNullOrEmpty();
            result.data.Should().Be("{\"Id\":12,\"Name\":\"Test\"}");
            result.json.Should().NotBeNull();
            result.json.Id.Should().Be(12);
            result.json.Name.Should().Be("Test");
        }
    }
}
