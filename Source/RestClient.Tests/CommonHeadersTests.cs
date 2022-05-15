using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Salix.RestClient;
using Xunit;

namespace RestClient.Tests
{
    public class CommonHeadersTests
    {
        private HttpRequestMessage _requestMessage;
        private readonly ILogger _logger;
        private readonly Mock<IHttpClientFactory> _factoryMock;
        private readonly HttpClient _fakeHttpClient;
        private readonly Mock<HttpMessageHandler> _messageHandlerMock;

        public CommonHeadersTests()
        {
            _messageHandlerMock = new Mock<HttpMessageHandler>();
            _messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>(
                    (httpRequestMessage, cancellationToken) =>
                    {
                        _requestMessage = httpRequestMessage;
                        if (httpRequestMessage.Content != null)
                        {
                            httpRequestMessage.Content
                                .ReadAsStringAsync(cancellationToken)
                                .GetAwaiter()
                                .GetResult();
                        }
                    })
                .Returns(
                    Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("\"2021-08-12T00:00:00Z\""),
                        RequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://mypc/webapi/testresponse")),
                    }));

            _factoryMock = new Mock<IHttpClientFactory>();
            _fakeHttpClient = new HttpClient(_messageHandlerMock.Object);
            _factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_fakeHttpClient);
            _logger = new Mock<ILogger>().Object;
        }

        [Fact]
        public async Task CommonHeaders_AddedToRequest()
        {
            var testable = new HeaderClient(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi"
                },
                _logger);

            await testable.GetAsync("base");
            _requestMessage.Headers.Should().HaveCount(3); // + Default Accept
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Locale")
                .Value.FirstOrDefault().Should().Be("lv-LV");
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "ClientId")
                .Value.FirstOrDefault().Should().Be("ABC123");
        }

        [Fact]
        public async Task CommonHeaders_Added_WithSettings()
        {
            var testable = new HeaderClient(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
                },
                _logger);

            await testable.GetAsync("base");
            _requestMessage.Headers.Should().HaveCount(4);
        }

        [Fact]
        public async Task CommonHeaders_Overriding_Settings()
        {
            var testable = new HeaderClient(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string> { { "Locale", "en-GB" } }
                },
                _logger);

            await testable.GetAsync("base");
            _requestMessage.Headers.Should().HaveCount(3); // + Default Accept
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Locale")
                .Value.FirstOrDefault().Should().Be("lv-LV");
        }

        [Fact]
        public async Task RequestHeaders_Overriding_Common()
        {
            var testable = new HeaderClient(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string> { { "Locale", "en-GB" } }
                },
                _logger);

            await testable.GetAsync("base", null, null, null, new Dictionary<string, string> { { "Locale", "es-MX" } });
            _requestMessage.Headers.Should().HaveCount(3); // + Default Accept
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Locale")
                .Value.FirstOrDefault().Should().Be("es-MX");
        }
    }

    [ExcludeFromCodeCoverage]
    public class HeaderClient : AbstractRestClient
    {
        public HeaderClient(IHttpClientFactory httpClientFactory, RestServiceSettings parameters, ILogger logger)
            : base(httpClientFactory, parameters, logger, NewtonsoftJsonObjectSerializer.Default)
        { }

        protected override (string Key, string Value) GetAuthenticationKeyValue() => new("Bearer", "9285293453");

        protected override Dictionary<string, string> GetCommonHeaders() =>
            new() { { "Locale", "lv-LV" }, { "ClientId", "ABC123" } };
    }

}
