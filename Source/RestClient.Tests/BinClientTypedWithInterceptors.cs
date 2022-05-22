using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Tests
{
    /// <summary>
    /// Test client to https://httpbin.org for integration testing (actual calls).
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BinClientTypedWithInterceptors : AbstractRestClient
    {
        public BinClientTypedWithInterceptors(HttpClient httpClient, RestServiceSettings settings, ILogger logger) : base(httpClient, settings, logger)
        {
        }

        public bool ReThrow { get; set; }

        public bool HasException { get; private set; }
        public string ExceptionType { get; private set; }
        public bool HasResponse { get; private set; }
        public Exception? Failure { get; private set; }

        protected override void InterceptRequestBeforeCall(HttpRequestMessage request)
        {
            request.Headers.Add("More", "Interceptors");
        }

        protected override bool InterceptResponseAfterCall(HttpResponseMessage response, Exception exception)
        {
            if (exception != null)
            {
                this.HasException = true;
                this.ExceptionType = exception.GetType().Name;
                this.Failure = exception;
            }

            if (response != null)
            {
                this.HasResponse = true;
            }

            return ReThrow;
        }
    }
}
