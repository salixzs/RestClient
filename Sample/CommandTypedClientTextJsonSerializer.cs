using Salix.Extensions;

namespace RestClient.Sample;

public class CommandTypedClientTextJsonSerializer : IConsoleOperation
{
    private readonly TypedClientTextJsonSerializer _client;
    public string OperationName => "serializer";
    public string HelpText => "Client test by using System.Text.Json serializer.";

    public CommandTypedClientTextJsonSerializer(TypedClientTextJsonSerializer client) => _client = client;

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
