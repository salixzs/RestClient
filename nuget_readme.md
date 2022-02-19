# Salix.RestClient

Wrapper around IHttpClientFactory HttpClient in System.Net.Http (Core 3+) to provide easier access to RESTful APIs from backend .Net code.\
Package provides abstract base class for any of 3 usage types - with Factory, Named client/factory and Typed client to be used to implement your own REST service HttpClient.\
Included HttpClient extensions - shortcuts to do  GET, POST, PUT, PATCH and DELETE operations with variety of parameters.\
Operations can return raw `HttpResponseMessage` or directly deserialized - typed data object. Internally it uses Json.Net (default) which can be switched to use `System.Text.Json` deserializer.
As parameters you can supply path variables and/or query parameters in addition to data content. These are wrapped into own managing objects to simplify their usage.\
Example:

```text
/api/parents/{id}/children/{childId}?skip=0&take=10
```
can be executed as this GET extension method call, returning strongly typed `DomainObject`:
```c#
var result = await _client.GetAsync<DomainObject>("/api/parents/{id}/children/{childId}", new { id = 12, childId = 15 }, new QueryParameterCollection {{ "skip", 0 },{ "take", 10 }})
```

Package expects it to have concrete settings object, from witch it gathers `BaseAddress`, [optionally] factory name, authentication approach, any default headers ("text/json" is added by default, if not specified implicitly).\
Custom `RestClientException` is thrown on failures, containing all the information about request failure.\
Built in timer will get you execution time you can read after each request for any monitoring needs.\
Development time verbose logging will output plenty of details on internal work to logging output (Use Debug() to see it in Visual Studio output window).


## Usage

> More detailed documentation is in [Wiki](https://github.com/salixzs/RestClient/wiki).

Create your own API client class, deriving from `AbstractRestClient` class and use any of four constructors, depending on your approach - constructors with IHttpClientFactory for factory or named approaches or constructors with HttpClient instance for typed clients.

```csharp
public class MyServiceClient : AbstractRestClient
{
    /// <summary>
    /// Client to work with MyService API. Here assumed typed client implementation/setup with default Json.Net serizalizer.
    /// </summary>
    public MyServiceClient(HttpClient httpClient, MyServiceClientSettings parameters, ILogger<MyServiceClient> logger)
        : base(httpClient, parameters, logger)
    {
    }
}
```

Client needs settings to be defined (derived from `RestServiceSettings`) to know where and how to connect to API:

```csharp
// Yepp. Nothing required for this class if nothing special is needed.
public class MyServiceClientSettings : RestServiceSettings
{
}
```

For this you need to create this class instance supplying base class property values (from configuration). The only required property to set is `BaseAddress`. All other are optional if you want to change authentication mechanism or add some default header key-values for all API calls.

```csharp
new MyServiceClientSettings {
    BaseAddress = "https://api.myservice.com",
    Authentication = new RestServiceAuthentication
    {
        AuthenticationType = ApiAuthenticationType.Basic,
        UserName = "apiuser",
        Password = "secret"
    },
    FactoryName = "namedClient",
    RequestHeaders = new Dictionary<string, string> { { "version", "2.0" } }
}
```

Using MS Extensions for Dependency Injection, register them appropriately

```csharp
services.AddHttpClient<MyServiceClient>(); // This registers IHttpClientFactory with typed client
services.AddSingleton(myServiceClientSettings); // instance of configuration for API client
```

Then use client injected instance in your logic/controller/handler classes:

```csharp
public class BusinessLogic
{
    private readonly MyServiceClient _api;
    
    // ctor
    public BusinessLogic(MyServiceClient apiClient) => _api = apiClient;
    
    public async Task LogicMethod() 
    {
        var receivedData = await _api.GetAsync<MyData>("endpoint/data/uri");
    }
}
```
