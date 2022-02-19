using Salix.Extensions;

namespace RestClient.Sample;

public class CommandTypedClientWithInterface : IConsoleOperation
{
    private readonly ITypedClientInterface _client;
    public string OperationName => "typed";
    public string HelpText => "Typed client test.";

    public CommandTypedClientWithInterface(ITypedClientInterface client) => _client = client ?? throw new ArgumentNullException(nameof(client));

    public async Task<int> DoWork()
    {
        Consolix.WriteLine("Interfaced client calls to get GUIDs from https://httpbin.org", ConsoleColor.Green);

        var clientResult = await _client.GetUuid();
        Consolix.WriteLine("Client method call to get uuid returned: {0}", clientResult, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        var directResult = await _client.GetAsync<IpAddress>("ip");
        Consolix.WriteLine("Your IP address is: {0}", directResult.origin, ConsoleColor.DarkYellow, ConsoleColor.Cyan);

        return 0;
    }

    public bool IsReady => true;
}
