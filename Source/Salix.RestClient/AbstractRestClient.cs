using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient
{
    /// <summary>
    /// Abstract wrapper around HttpClient instance.
    /// </summary>
    public class AbstractRestClient : HttpClientExtender
    {
        /// <summary>
        /// Creates REST client by passing in HttpClient directly (Typed clients) with default Json.Net serializer use.
        /// </summary>
        protected AbstractRestClient(HttpClient httpClient, RestServiceSettings settings, ILogger logger) : this(httpClient, settings, logger, null)
        {
        }

        /// <summary>
        /// Creates REST client by passing in HttpClient directly (Typed clients) with supplied serializer.
        /// </summary>
        protected AbstractRestClient(HttpClient httpClient, RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer) : base(settings, logger, serializer)
        {
            this.HttpClientInstance = httpClient;
            this.HttpClientInstance.BaseAddress = new Uri(settings.BaseAddress);
        }

        /// <summary>
        /// Creates REST client by passing HttpClientFactory (factory and named clients) with default Json.Net serializer use.
        /// </summary>
        protected AbstractRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger)
            : this(httpClientFactory, settings, logger, null)
        {
        }

        /// <summary>
        /// Creates REST client by passing HttpClientFactory (factory and named clients) with supplied serializer.
        /// </summary>
        protected AbstractRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer)
            : base(settings, logger, serializer)
        {
            this.HttpClientInstance = string.IsNullOrEmpty(settings.FactoryName) ? httpClientFactory.CreateClient() : httpClientFactory.CreateClient(settings.FactoryName);
            this.HttpClientInstance.BaseAddress = new Uri(settings.BaseAddress);
        }
    }
}
