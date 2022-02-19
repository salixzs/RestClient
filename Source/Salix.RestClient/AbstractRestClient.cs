using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient
{
    /// <summary>
    /// Abstract wrapper around HttpClient instance to be inherited by your own client class.
    /// <code>
    /// public class MyApiClient : AbstractRestClient
    /// 
    /// // or with interface
    /// public class MyApiClient : AbstractRestClient, IMyApiClient
    /// // ..where
    /// public interface IMyApiClient : IRestClient
    /// </code>
    /// </summary>
    public abstract class AbstractRestClient : HttpClientExtender
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
            this.SetupHttpClient(settings);
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
            this.SetupHttpClient(settings);
        }

        /// <summary>
        /// Sets up the 
        /// </summary>
        /// <param name="settings"></param>
        private void SetupHttpClient(RestServiceSettings settings)
        {
            this.HttpClientInstance.BaseAddress = new Uri(settings.BaseAddress);
            foreach (var defaultHeader in settings.RequestHeaders)
            {
                this.HttpClientInstance.DefaultRequestHeaders.Add(defaultHeader.Key, defaultHeader.Value);
            }
        }
    }
}
