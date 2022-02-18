using Microsoft.Extensions.Logging;
using Salix.RestClient;

namespace RestClient.Sample;

public class TypedClientWithInterface : AbstractTypedRestClient, ITypedClientInterface
{
    public TypedClientWithInterface(HttpClient httpClient, ClientSettings settings, ILogger<TypedClient> logger) : base(httpClient, settings, logger)
    { }

    public async Task<Guid> GetUuid()
    {
        var result = await this.GetAsync<BinUuid>("uuid");
        return result.uuid;
    }
}

public interface ITypedClientInterface : IRestClient
{
    Task<Guid> GetUuid();
}
