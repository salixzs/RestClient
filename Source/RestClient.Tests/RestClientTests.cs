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
    [ExcludeFromCodeCoverage]
    public class RestClientTests
    {
        private HttpRequestMessage _requestMessage;
        private readonly ILogger _logger;
        private readonly Mock<IHttpClientFactory> _factoryMock;
        private readonly HttpClient _fakeHttpClient;
        private readonly Mock<HttpMessageHandler> _messageHandlerMock;

        public RestClientTests()
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
        public async Task Create_Default_CorrectUrlAndMediaType()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings { BaseAddress = "http://mypc/webapi/" },
                _logger);

            await testable.GetAsync("any/operation");
            _fakeHttpClient.BaseAddress.AbsoluteUri.Should().Be("http://mypc/webapi/");
            _requestMessage.Headers.Accept.Should().HaveCount(1);
            _requestMessage.Headers.Accept.First().MediaType.Should().Be("application/json");
        }

        [Fact]
        public async Task Get_JustUrl_RequestHasCorrectURL()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings { BaseAddress = "http://mypc/webapi/" },
                _logger);
            await testable.GetAsync("base");
            _requestMessage.RequestUri.AbsolutePath.Should().Be("/webapi/base");
        }

        [Fact]
        public async Task Get_UrlPlaceholders_RequestHasCorrectURL()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings { BaseAddress = "http://mypc/webapi/" },
                _logger);
            await testable.GetAsync("base/{id}/sub/{key}", new PathParameters("id", 777, "key", "abc"));
            _requestMessage.RequestUri.AbsolutePath.Should().Be("/webapi/base/777/sub/abc");
        }

        [Fact]
        public async Task Get_QueryParams_RequestHasCorrectURL()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings { BaseAddress = "http://mypc/webapi/" },
                _logger);
            await testable.GetAsync<DateTime>("base", null, new QueryParameters { new QueryParameter("filter", "all"), new QueryParameter("page", 3) }, null);
            _requestMessage.RequestUri.PathAndQuery.Should().Be("/webapi/base?filter=all&page=3");
        }

        [Fact]
        public async Task Get_AllParams_RequestHasCorrectURL()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings { BaseAddress = "http://mypc/webapi/" },
                _logger);
            await testable.GetAsync<DateTime>(
                "person/{id}",
                new PathParameters("id", 102332),
                new QueryParameters {
                    new QueryParameter("filter", "active"),
                    new QueryParameter("page", 2) });
            _requestMessage.RequestUri.PathAndQuery.Should().Be("/webapi/person/102332?filter=active&page=2");
        }

        [Fact]
        public async Task Create_XMLAndCustom_IsSet()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string>
                        {
                            { "Accept", "string/xml" },
                            { "Correlate", "AGENT007" }
                        }
                },
                _logger);

            await testable.GetAsync("base");
            _requestMessage.Headers.Accept.Should().HaveCount(1);
            _requestMessage.Headers.Accept.First().MediaType.Should().Be("string/xml");
            var customHeader =
                _requestMessage.Headers.FirstOrDefault(h => h.Key == "Correlate");
            customHeader.Should().NotBeNull();
            customHeader.Value.FirstOrDefault().Should().NotBeNull();
            customHeader.Value.FirstOrDefault().Should().Be("AGENT007");
            var auth = _requestMessage.Headers.Authorization;
            auth.Should().BeNull();
        }

        [Fact]
        public async Task DefaultRequestHeaders_Get_ReturnsHttpClientHeaders()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
                },
                _logger);

            await testable.GetAsync("base");
            _requestMessage.Headers.Should().HaveCount(2);
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Existing")
                .Value.FirstOrDefault().Should().Be("1");
        }

        [Fact]
        public async Task ChangeHeader_Existing_IsChanged()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
                },
                _logger);

            await testable.GetAsync("base", null, null, null, new Dictionary<string, string> { { "Existing", "2" } });
            _requestMessage.Headers.Should().HaveCount(2);
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Existing")
                .Value.Should().HaveCount(1);
            _requestMessage.Headers.FirstOrDefault(h => h.Key == "Existing")
                .Value.FirstOrDefault().Should().Be("2");
        }

        [Fact]
        public async Task Create_BasicAuthentication_IsSet()
        {
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi",
                    Authentication = new RestServiceAuthentication
                    {
                        AuthenticationType = ApiAuthenticationType.Basic,
                        UserName = "jons",
                        Password = "plunts"
                    }
                },
                _logger);
            await testable.GetAsync("any/operation");
            var auth = _requestMessage.Headers.Authorization;
            auth.Should().NotBeNull();
            auth.Scheme.Should().Be("Basic");
            auth.Parameter.Should().Be("am9uczpwbHVudHM=");
        }

        [Fact]
        public void PostAsync_Inheritance_AllFieldsSerializedForSending()
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi/",
                    RequestHeaders = new Dictionary<string, string> { { "SID", "111111" } }
                },
                _logger);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("1000")
            };
            var data = new TestWrapper
            {
                TestField = new TestExtended
                {
                    Id = 1001,
                    Name = "Testing",
                    Description = "Description here",
                    ModificationTime = DateTime.Now
                }
            };
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(httpResponse))
                .Callback((HttpRequestMessage request, CancellationToken token) =>
                {
                    request.Should().NotBeNull();
                    request.Content.Should().NotBeNull();

                    request.Headers.Should().HaveCount(3);
                    request.Headers.First(h => h.Key == "SID").Value.FirstOrDefault().Should().Be("111111");
                    request.Headers.First(h => h.Key == "user").Value.FirstOrDefault().Should().Be("tester");

                    request.RequestUri.AbsoluteUri.Should().Be("http://mypc/webapi/test/1?nocache=true");

                    var serializedContent = request.Content.ReadAsStringAsync(CancellationToken.None).Result;
                    serializedContent.Should().NotBeNullOrEmpty();
                    serializedContent.Contains("ModificationTime").Should().BeTrue();
                });

            var result = testable.PostAsync<long>(
                    "test/{id}",
                    data, new PathParameters("id", 1),
                    new QueryParameters
                    {
                        new QueryParameter("nocache", "true")
                    },
                    new Dictionary<string, string> { { "user", "tester" } })
                .Result;
            result.Should().Be(1000);
        }

        [Fact]
        public void GetAsync_ErrorReturned_RawDataAddedToException()
        {
            var logger = new Mock<ILogger>();
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi/"
                },
                logger.Object);
            var error = "something wrong";
            var serializedError = SystemTextJsonObjectSerializer.Default.SerializeAsync(error).Result;
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(serializedError),
                RequestMessage = new HttpRequestMessage { Method = HttpMethod.Get },
            };
            var data = new TestWrapper
            {
                TestField = new TestExtended
                {
                    Id = 1001,
                    Name = "Testing",
                    Description = "Description here",
                    ModificationTime = DateTime.Now
                }
            };
            _messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(httpResponse));

            Action action = () => testable.PostAsync<long>("test", data).Wait();
            action.Should().Throw<RestClientException>().And.Data["Api.RawError"].Should().Be(serializedError);
        }

        [Fact]
        public void GetAsync_WrongData_SerializationException()
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            var testable = new TestClientFactory(
                _factoryMock.Object,
                new RestServiceSettings
                {
                    BaseAddress = "http://mypc/webapi/"
                },
                _logger);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("1000") // Not expected return JSON object
            };
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(httpResponse));

            Action action = () => testable.GetAsync<Test>("test").Wait();
            action.Should().Throw<RestClientException>().WithMessage(@"Error occurred while deserializing API response to RestClient.Tests.Test.
Make sure you are calling correct operation and deserializing result to correct type.
Request status code: 200 (OK).
--- ---");
        }
    }


    [ExcludeFromCodeCoverage]
    public class Test
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TestExtended : Test
    {
        public DateTime ModificationTime { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TestWrapper
    {
        public Test TestField { get; set; }
    }
}
