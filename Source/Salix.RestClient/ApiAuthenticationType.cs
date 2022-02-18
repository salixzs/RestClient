namespace Salix.RestClient;

/// <summary>
/// Should be used to specify authentication type for REST client.
/// </summary>
public enum ApiAuthenticationType
{
    /// <summary>
    /// API does not use any authentication or authentication is done via intercepting handler.
    /// </summary>
    None = 0,

    /// <summary>
    /// API uses Basic authentication = username:password
    /// </summary>
    Basic = 1,

    /// <summary>
    /// API uses either Bearer, ApiKey or other Key-Value pairs which should be added to Authorization header.
    /// NOTE: Client should implement method to get this key-value pair.
    /// </summary>
    External = 2,
}
