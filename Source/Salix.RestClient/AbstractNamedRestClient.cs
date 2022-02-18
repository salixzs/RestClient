using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

/// <summary>
/// Generic, typed REST (API) Service client instance.
/// </summary>
public abstract class AbstractNamedRestClient : HttpClientExtender
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Name of the named HttpClientFactory.
    /// </summary>
    protected abstract string ClientName { get; }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractNamedRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger) : this(httpClientFactory, settings, logger, null)
    {
    }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractNamedRestClient(IHttpClientFactory httpClientFactory, RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer) : base(settings, logger, serializer) =>
        _httpClientFactory = httpClientFactory;

    /// <summary>
    /// Method to get a HttpClient from named HttpClientFactory.
    /// </summary>
    protected override HttpClient GetHttpClient()
    {
        if (string.IsNullOrEmpty(this.ClientName))
        {
            throw new RestClientException("Named client should have name set.");
        }

        return _httpClientFactory.CreateClient(this.ClientName);
    }
}
