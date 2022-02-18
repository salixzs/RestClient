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
    /// Base URL of REST Service (API endpoint).
    /// </summary>
    public string BaseAddress { get; set; } = null!;

    /// <summary>
    /// Sets authentication type and authentication parameters for REST API calls.
    /// </summary>
    public RestServiceAuthentication Authentication { get; set; } = new RestServiceAuthentication();

    /// <summary>
    /// Request Headers to add to HTTP request when calling API (REST service).
    /// Key - Header name, Value - Header value.
    /// <code>
    /// { "Accept", "application/json" }, { "User-Agent", "EFormsFrontend" }
    /// </code>
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();

    private string DebuggerDisplay => $"URL: {this.BaseAddress}, Auth: {this.Authentication.AuthenticationType}, Header count: {this.RequestHeaders.Count}";
}
