using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        [Fact]
        public void Create_Default_CorrectUrlAndMediaType()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(
                new HttpClient(),
                new RestServiceSettings { ServiceUrl = "http://mypc/webapi/" },
                logger.Object);
            testable.Client.BaseAddress.AbsoluteUri.Should().Be("http://mypc/webapi/");
            testable.Client.DefaultRequestHeaders.Accept.Should().HaveCount(1);
            testable.Client.DefaultRequestHeaders.Accept.First().MediaType.Should().Be("application/json");
        }

        [Fact]
        public async Task Get_JustUrl_RequestHasCorrectURL()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(
                new HttpClient(),
                new RestServiceSettings { ServiceUrl = "http://mypc/webapi/" },
                logger.Object);
            await testable.GetAsync<DateTime>("base", null, null);
            testable.LastCalledUrl.Should().Be("base");
        }

        [Fact]
        public async Task Get_UrlPlaceholders_RequestHasCorrectURL()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(
                new HttpClient(),
                new RestServiceSettings { ServiceUrl = "http://mypc/webapi/" },
                logger.Object);
            await testable.GetAsync<DateTime>("base/{id}/sub/{key}", new { id = 777, key = "abc" }, null);
            testable.LastCalledUrl.Should().Be("base/777/sub/abc");
        }

        [Fact]
        public async Task Get_QueryParams_RequestHasCorrectURL()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(
                new HttpClient(),
                new RestServiceSettings { ServiceUrl = "http://mypc/webapi/" },
                logger.Object);
            await testable.GetAsync<DateTime>(
                "base",
                null,
                new QueryParameterCollection { new QueryParameter("filter", "all"), new QueryParameter("page", 3) });
            testable.LastCalledUrl.Should().Be("base?filter=all&page=3");
        }

        [Fact]
        public async Task Get_AllParams_RequestHasCorrectURL()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(
                new HttpClient(),
                new RestServiceSettings { ServiceUrl = "http://mypc/webapi/" },
                logger.Object);
            await testable.GetAsync<DateTime>(
                "person/{id}",
                new { id = 102332 },
                new QueryParameterCollection {
                    new QueryParameter("filter", "active"),
                    new QueryParameter("page", 2) });
            testable.LastCalledUrl.Should().Be("person/102332?filter=active&page=2");
        }

        [Fact]
        public void Create_XMLAndCustom_IsSet()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(new HttpClient(), new RestServiceSettings
            {
                ServiceUrl = "http://mypc/webapi",
                RequestHeaders = new Dictionary<string, string>
                    {
                        { "Accept", "string/xml" },
                        { "Correlate", "AGENT007" }
                    }
            }, logger.Object);
            testable.Client.DefaultRequestHeaders.Accept.Should().HaveCount(1);
            testable.Client.DefaultRequestHeaders.Accept.First().MediaType.Should().Be("string/xml");
            var customHeader =
                testable.Client.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "Correlate");
            customHeader.Should().NotBeNull();
            customHeader.Value.FirstOrDefault().Should().NotBeNull();
            customHeader.Value.FirstOrDefault().Should().Be("AGENT007");
            var auth =
                testable.Client.DefaultRequestHeaders.Authorization;
            auth.Should().BeNull();
        }

        [Fact]
        public void DefaultRequestHeaders_Get_ReturnsHttpClientHeaders()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(new HttpClient(), new RestServiceSettings
            {
                ServiceUrl = "http://mypc/webapi",
                RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
            }, logger.Object);

            testable.DefaultRequestHeaders.Should().HaveCount(2);
            testable.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "Existing")
                .Value.FirstOrDefault().Should().Be("1");
        }

        [Fact]
        public void AddOrUpdateDefaultHeader_New_IsAdded()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(new HttpClient(), new RestServiceSettings
            {
                ServiceUrl = "http://mypc/webapi",
                RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
            }, logger.Object);

            testable.AddOrUpdateDefaultRequestHeader("New", "2");
            testable.Client.DefaultRequestHeaders.Should().HaveCount(3);
            testable.Client.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "Existing")
                .Value.FirstOrDefault().Should().Be("1");
            testable.Client.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "New")
                .Value.FirstOrDefault().Should().Be("2");
        }

        [Fact]
        public void AddOrUpdateDefaultHeader_Existing_IsChanged()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(new HttpClient(), new RestServiceSettings
            {
                ServiceUrl = "http://mypc/webapi",
                RequestHeaders = new Dictionary<string, string> { { "Accept", "string/xml" }, { "Existing", "1" } }
            }, logger.Object);

            testable.AddOrUpdateDefaultRequestHeader("Existing", "2");
            testable.Client.DefaultRequestHeaders.Should().HaveCount(2);
            testable.Client.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "Existing")
                .Value.Should().HaveCount(1);
            testable.Client.DefaultRequestHeaders.FirstOrDefault(h => h.Key == "Existing")
                .Value.FirstOrDefault().Should().Be("2");
        }

        [Fact]
        public void Create_BasicAuthentication_IsSet()
        {
            var logger = new Mock<ILogger>();
            var testable = new TestClient(new HttpClient(), new RestServiceSettings
            {
                ServiceUrl = "http://mypc/webapi",
                Authentication = new RestServiceAuthentication
                {
                    AuthenticationType = ApiAuthenticationType.Basic,
                    UserName = "jons",
                    Password = "plunts"
                }
            }, logger.Object);
            var auth = testable.Client.DefaultRequestHeaders.Authorization;
            auth.Should().NotBeNull();
            auth.Scheme.Should().Be("Basic");
            auth.Parameter.Should().Be("am9uczpwbHVudHM=");
        }

        [Fact]
        public void PostAsync_Inheritence_AllFieldsSerializedForSending()
        {
            var logger = new Mock<ILogger>();
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            var testable = new TestClient(
                new HttpClient(httpMessageHandler.Object),
                new RestServiceSettings
                {
                    ServiceUrl = "http://mypc/webapi/",
                    RequestHeaders = new Dictionary<string, string> { { "SID", "111111" } }
                },
                logger.Object);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
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
            httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
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
                    data, new { id = "1" },
                    new QueryParameterCollection
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
            var testable = new TestClient(
                new HttpClient(httpMessageHandler.Object),
                new RestServiceSettings
                {
                    ServiceUrl = "http://mypc/webapi/"
                },
                logger.Object);
            var error = "something wrong";
            var serializedError = SystemTextJsonObjectSerializer.Default.SerializeAsync(error).Result;
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
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
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(httpResponse));

            Action action = () => testable.PostAsync<long>("test", data).Wait();
            action.Should().Throw<RestClientException>().And.Data["Api.RawError"].Should().Be(serializedError);
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
