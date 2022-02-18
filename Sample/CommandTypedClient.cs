using Salix.Extensions;

namespace RestClient.Sample;

public class CommandTypedClient : IConsoleOperation
{
    private readonly TypedClient _client;
    public string OperationName => "typed";
    public string HelpText => "Typed client test.";

    public CommandTypedClient(TypedClient client) => _client = client;

    public async Task<int> DoWork()
    {
        Consolix.WriteLine("Typed client call to get GUID from https://httpbin.org", ConsoleColor.Green);

        var uuid = await _client.GetAsync<BinUuid>("uuid");
        Consolix.WriteLine("Call to uuid returned: {0}", uuid.uuid, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        var ip = await _client.GetAsync<IpAddress>("ip");
        Consolix.WriteLine("Your IP address is: {0}", ip.origin, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        return 0;
    }

    public bool IsReady => true;
}
