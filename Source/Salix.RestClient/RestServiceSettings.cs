using System.Collections.Generic;
using System.Diagnostics;

namespace Salix.RestClient;

/// <summary>
/// Type describing REST main request information, like operation URL and Header contents.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class RestServiceSettings
{
    /// <summary>
    /// Base URL of REST Service (like https://localhost or https://myapi.azurewebsites.net or http://10.123.32.1/inst2).<br/>
    /// This is MANDATORY setting to be set!
    /// </summary>
    public string BaseAddress { get; set; } = null!;

    /// <summary>
    /// For NAMED factory/client - set this to desired name to be used.<br/>
    /// Reuse it on Factory registration:<br/>
    /// <code>
    /// services.AddHttpClient(restServiceSettings.FactoryName);
    /// </code>
    /// and your RestClient instance with IHttpClientFactory parameter.
    /// </summary>
    public string? FactoryName { get; set; }

    /// <summary>
    /// Sets authentication type and authentication parameters for REST API calls.
    /// </summary>
    public RestServiceAuthentication Authentication { get; set; } = new RestServiceAuthentication();

    /// <summary>
    /// Request Headers to add to HTTP request when calling API (REST service).<br/>
    /// Key - Header name, Value - Header value.<br/><br/>
    /// <code>
    /// { "Accept", "application/json" }, { "User-Agent", "MyFrontend" }
    /// </code>
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();

    private string DebuggerDisplay => $"URL: {this.BaseAddress}, Auth: {this.Authentication.AuthenticationType}, Header count: {this.RequestHeaders.Count}";
}
