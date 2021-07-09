# Salix.RestClient

Wrapper for RESTful service (API) client, using IHttpClientFactory in ASP.NET framework. Package provides abstract base class to be used in actual client implementations.

## Usage

Create your own API client, deriving from `AbstractRestClient` class.

```csharp
public class MyServiceClient : AbstractRestClient
{
    /// <summary>
    /// Client to work with MyService API.
    /// </summary>
    /// <param name="httpClient">The HTTP client (.Net Framework).</param>
    /// <param name="parameters">The parameters for MyService API client.</param>
    /// <param name="logger">The logging object (ILogger in MS.Extensions.Logging).</param>
    public MyServiceClient(HttpClient httpClient, MyServiceClientSettings parameters, ILogger<MyServiceClient> logger)
        : base(httpClient, parameters, logger)
    {
    }
}
```

Client needs some settings (derived from `RestServiceSettings`) to know where and how to connect to API:

```csharp
// Yepp. Nothing required for this class if nothing special is needed.
public class MyServiceClientSettings : RestServiceSettings
{
}
```

For this you need to create this class instance supplying base class property values from configuration. The only required property to set is `ServiceUrl`. All other are optional if you want to change authentication mechanism or add some default header key-values for all API calls.

```csharp
new MyServiceClientSettings {
    ServiceUrl = "https://api.myservice.com",
    Authentication = new RestServiceAuthentication
    {
        AuthenticationType = ApiAuthenticationType.Basic,
        UserName = "apiuser",
        Password = "secret"
    },
    RequestHeaders = new Dictionary<string, string> { { "key", "value" } }
}
```

Using MS Extensions for Dependency Injection, register them appropriately

```csharp
services.AddSingleton(myServiceClientSettings); // instance of configuration for API client
services.AddHttpClient<MyServiceClient>("MyService");
```

Then use client injected instance in your business logic:

```csharp
public class BusinessLogic
{
    private readonly MyServiceClient _api;
    
    public BusinessLogic(MyServiceClient apiClient) => _api = apiClient;
    
    public async Task LogicMethod() 
    {
        var receivedData = await _api.GetAsync<MyData>("endpoint/data/uri");
    }
}
```

If you need to unit-test your business logic, mock your client dependencies:

```csharp
_clientMock = new Mock<MyServiceClient>(
                MockBehavior.Strict,
                new Mock<HttpClient>().Object,
                new MyServiceClientSettings { ServiceUrl = "http://localhost" },
                new Mock<ILogger<MyServiceClient>>().Object
            );

_clientMock.Setup(x => x.GetAsync<MyData>("endpoint/data/uri"))
                .Returns(Task.FromResult(new MyData
                {
                    SomeProp = "la la la"
                }));
```