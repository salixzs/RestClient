using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Salix.RestClient;
using Xunit;
using Xunit.Abstractions;

namespace RestClient.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationInterceptorTests
    {
        private readonly HttpClient _httpClient = new();
        private readonly XUnitLogger<BinClientTyped> _logger;
        private BinClientTypedWithInterceptors _api;

        public IntegrationInterceptorTests(ITestOutputHelper output) => _logger = new XUnitLogger<BinClientTyped>(output);

        [Fact]
        public async Task RequestInterceptor_AddedHeader_IsAdded()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var result = await _api.GetAsync<MethodResponse>("anything");
            result.Should().NotBeNull();
            result.headers.Should().NotBeEmpty();
            result.headers.Should().ContainKey("More");
            result.headers["More"].Should().Be("Interceptors");
        }

        [Fact]
        public async Task ResponseInterceptor_Success_ExceptionIsNull()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var result = await _api.GetAsync<MethodResponse>("anything");
            _api.HasResponse.Should().BeTrue();
            _api.HasException.Should().BeFalse();
        }

        [Fact]
        public async Task ResponseInterceptor_HostFailure_ExceptionIsNotNull()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://oehrveaoirhgsdf.fpr" }, _logger);
            var result = await _api.GetAsync<MethodResponse>("kjdfhg");
            _api.HasResponse.Should().BeTrue();
            _api.HasException.Should().BeTrue();
            _api.ExceptionType.Should().Be("HttpRequestException");
            _api.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ResponseInterceptor_EndpointFailure_ExceptionIsNotNull()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var result = await _api.GetAsync<MethodResponse>("kjdfhg");
            _api.HasResponse.Should().BeTrue();
            _api.HasException.Should().BeTrue();
            _api.ExceptionType.Should().Be("RestClientException");
            _api.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ResponseInterceptor_LongAndCancel_HasException()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            var cancelSource = new CancellationTokenSource();
            cancelSource.CancelAfter(TimeSpan.FromSeconds(3));
            var result = await _api.GetAsync<MethodResponse>("delay/10", cancelSource.Token);
            _api.HasResponse.Should().BeTrue();
            _api.HasException.Should().BeTrue();
            _api.ExceptionType.Should().Be("TaskCanceledException");
            _api.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ResponseInterceptor_ReThrowCancel_Rethrows()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger);
            _api.ReThrow = true;
            try
            {
                var cancelSource = new CancellationTokenSource();
                cancelSource.CancelAfter(TimeSpan.FromSeconds(3));
                var result = await _api.GetAsync<MethodResponse>("delay/10", cancelSource.Token);

                // Should not get here, so make fake Assert
                "Operation was success".Should().Be("Operation was cancelled.");
            }
            catch (TaskCanceledException ex)
            {
                // Still values are stored.
                _api.HasResponse.Should().BeTrue();
                _api.HasException.Should().BeTrue();
                _api.ExceptionType.Should().Be("TaskCanceledException");
                _api.StatusCode.Should().NotBe(HttpStatusCode.OK);
                return;
            }

            // Should not get here, so make fake Assert
            "Operation was success".Should().Be("Operation was cancelled.");
        }

        [Fact]
        public async Task ResponseInterceptor_ReThrowFailure_Rethrows()
        {
            _api = new BinClientTypedWithInterceptors(_httpClient, new RestServiceSettings { BaseAddress = "https://httpbin.org" }, _logger)
            {
                ReThrow = true
            };
            try
            {
                var result = await _api.GetAsync<MethodResponse>("kjdfhg");
            }
            catch (RestClientException exc)
            {
                _api.HasResponse.Should().BeTrue();
                _api.HasException.Should().BeTrue();
                _api.ExceptionType.Should().Be("RestClientException");
                _api.StatusCode.Should().Be(HttpStatusCode.NotFound);
                return;
            }

            // Should not get here, so make fake Assert
            "Operation was success".Should().Be("Operation was cancelled.");
        }
    }
}
