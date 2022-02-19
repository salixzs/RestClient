using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Sample;

public class FactoryClient : AbstractRestClient
{
    public FactoryClient(IHttpClientFactory httpClientFactory, ClientSettings settings, ILogger<FactoryClient> logger) : base(httpClientFactory, settings, logger)
    { }
}
