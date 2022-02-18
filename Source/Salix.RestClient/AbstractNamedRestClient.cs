using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

/// <summary>
/// Generic, typed REST (API) Service client instance.
/// </summary>
public abstract class AbstractNamedRestClient : HttpClientExtender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private HttpClient? _httpClient;

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
    protected override HttpClient GetHttpClient(Uri baseAddress)
    {
        if (string.IsNullOrEmpty(this.ClientName))
        {
            throw new RestClientException("Named client should have name set.");
        }

        if (_httpClient != null)
        {
            return _httpClient;
        }

        _httpClient = _httpClientFactory.CreateClient(this.ClientName);
        _httpClient.BaseAddress = baseAddress;
        return _httpClient;
    }
}
