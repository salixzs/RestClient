using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

/// <summary>
/// Generic, typed REST (API) Service client instance.
/// </summary>
public abstract class AbstractFactoryRestClient : HttpClientExtender
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractFactoryRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger)
        : this(httpClientFactory, settings, logger, null)
    {
    }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractFactoryRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer)
        : base(settings, logger, serializer) =>
        _httpClientFactory = httpClientFactory;

    /// <summary>
    /// Method to get a HttpClient from named HttpClientFactory.
    /// </summary>
    protected override HttpClient GetHttpClient() => _httpClientFactory.CreateClient();
}
