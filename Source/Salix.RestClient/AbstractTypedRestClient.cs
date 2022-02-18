using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Salix.RestClient;

/// <summary>
/// Generic, typed REST (API) Service client instance.
/// </summary>
public abstract class AbstractTypedRestClient : HttpClientExtender
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractTypedRestClient(HttpClient httpClient, RestServiceSettings settings, ILogger logger) : this(httpClient, settings, logger, null)
    {
    }

    /// <summary>
    /// Generic, typed REST (API) Service client instance.
    /// </summary>
    protected AbstractTypedRestClient(HttpClient httpClient, RestServiceSettings settings, ILogger logger, IObjectSerializer? serializer) : base(settings, logger, serializer) =>
        _httpClient = httpClient;

    /// <summary>
    /// Method to get the injected HttpClient.
    /// </summary>
    protected override HttpClient GetHttpClient(Uri baseAddress)
    {
        // Should not set base address twice
        if (_httpClient.BaseAddress == baseAddress)
        {
            return _httpClient;
        }

        _httpClient.BaseAddress = baseAddress;
        return _httpClient;
    }
}
