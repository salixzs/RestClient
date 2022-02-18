using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Sample;

public class NamedClient : AbstractNamedRestClient
{
    public NamedClient(IHttpClientFactory httpClientFactory, ClientSettings settings, ILogger<NamedClient> logger) : base(httpClientFactory, settings, logger)
    { }

    protected override string ClientName => "named";
}
