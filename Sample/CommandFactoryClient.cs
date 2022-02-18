using Salix.Extensions;

namespace RestClient.Sample;

public class CommandFactoryClient : IConsoleOperation
{
    private readonly FactoryClient _client;
    public string OperationName => "named";
    public string HelpText => "Named client test.";

    public CommandFactoryClient(FactoryClient client) => _client = client;

    public async Task<int> DoWork()
    {
        Consolix.WriteLine("Factory client call to get GUID from https://httpbin.org", ConsoleColor.Green);

        var uuid = await _client.GetAsync<BinUuid>("uuid");
        Consolix.WriteLine("Call to uuid returned: {0}", uuid.uuid, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        var ip = await _client.GetAsync<IpAddress>("ip");
        Consolix.WriteLine("Your IP address is: {0}", ip.origin, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        return 0;
    }

    public bool IsReady => true;
}
