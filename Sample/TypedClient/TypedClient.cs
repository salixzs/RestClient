using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Sample;

public class TypedClient : AbstractRestClient
{
    public TypedClient(HttpClient httpClient, ClientSettings settings, ILogger<TypedClient> logger) : base(httpClient, settings, logger)
    { }
}
