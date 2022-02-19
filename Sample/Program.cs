using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Salix.Extensions;
using Salix.RestClient;

namespace RestClient.Sample;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Set color scheme for Console application
        Consolix.SetColorScheme(ConsoleColorScheme.HalfDark);

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            // Wire up Dependency injection container
            .UseConsoleLifetime()
            .ConfigureServices((context, collection) => SetupContainer(context, collection))
            .Build();

        try
        {
            var consoleOperationHandler = host.Services.GetRequiredService<ConsoleOperationHandler>();
            consoleOperationHandler.PrepareOperation(args); // selects chosen (or the only) operation and populates its parameters (if any)
            if (args.Contains("--h") || args.Contains("--help"))
            {
                consoleOperationHandler.OutputHelp(
                    typeof(Program).Assembly.GetName().Name,
                    "RestClient test sample application.");
                return 0;
            }

            // If we have chosen operation and it has all data it needs, invoke it.
            if (consoleOperationHandler.SelectedOperation is { IsReady: true })
            {
                // Here operation is called to do its work.
                return await consoleOperationHandler.SelectedOperation.DoWork();
            }

            // Fallback to displaying Help.
            consoleOperationHandler.OutputHelp(
                typeof(Program).Assembly.GetName().Name,
                "RestClient test sample application.");
            return -1;
        }
        catch (Exception ex)
        {
            // Let consolix output error in red color!
            Consolix.WriteLine(ex.Message, ConsoleColor.Red);
            return -1;
        }
    }

    private static void SetupContainer(HostBuilderContext context, IServiceCollection services)
    {
        // These normally are composed of values from application configuration
        var restClientSettings = new ClientSettings
        {
            BaseAddress = "https://httpbin.org",
            Authentication = new RestServiceAuthentication { AuthenticationType = ApiAuthenticationType.None },
            RequestHeaders = new Dictionary<string, string> { { "check", "this" } }
        };

        // <---- Uncomment registration groups for specific type of RestClient

        // TYPED client: Uncomment two lines below
        services.AddHttpClient<TypedClient>();
        services.AddTransient<IConsoleOperation, CommandTypedClient>();

        // NAMED client: Uncomment 4 lines below
        //restClientSettings.FactoryName = "named";
        //services.AddHttpClient(restClientSettings.FactoryName);
        //services.AddTransient<NamedClient>();
        //services.AddTransient<IConsoleOperation, CommandNamedClient>();

        // FACTORY client: Uncomment three lines below
        //services.AddHttpClient();
        //services.AddTransient<FactoryClient>();
        //services.AddTransient<IConsoleOperation, CommandFactoryClient>();

        // TYPED client (with INTERFACE): Uncomment two lines below
        //services.AddHttpClient<ITypedClientInterface, TypedClientWithInterface>();
        //services.AddTransient<IConsoleOperation, CommandTypedClientWithInterface>();

        // SERIALIZER change to System.Text.Json: Uncomment three lines below
        //services.AddScoped<IObjectSerializer, SystemTextJsonObjectSerializer>();
        //services.AddHttpClient<TypedClientTextJsonSerializer>();
        //services.AddTransient<IConsoleOperation, CommandTypedClientTextJsonSerializer>();

        // Required registrations
        services.AddTransient<ConsoleOperationHandler>();
        services.AddSingleton(restClientSettings);
    }
}
