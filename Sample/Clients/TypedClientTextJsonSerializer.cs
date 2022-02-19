using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Sample;

public class TypedClientTextJsonSerializer : AbstractRestClient
{
    public TypedClientTextJsonSerializer(HttpClient httpClient, ClientSettings settings, ILogger<TypedClientTextJsonSerializer> logger, IObjectSerializer serializer) : base(httpClient, settings, logger, serializer)
    { }
}
