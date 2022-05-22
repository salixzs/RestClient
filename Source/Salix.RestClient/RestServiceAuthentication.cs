using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Salix.RestClient;

/// <summary>
/// Authentication information (settings) for Rest Api Client.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class RestServiceAuthentication
{
    /// <summary>
    /// Sets authentication type and necessary values for API Client requests.
    /// </summary>
    public ApiAuthenticationType AuthenticationType { get; set; }

    /// <summary>
    /// Username for authentication when type is Basic.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Password (with <see cref="UserName"/>) for authentication when type is Basic.
    /// </summary>
    public string? Password { get; set; }

    [ExcludeFromCodeCoverage]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get
        {
            var dbg = new StringBuilder($"Auth: {this.AuthenticationType}");
            if (this.AuthenticationType == ApiAuthenticationType.Basic)
            {
                dbg.Append($" ({this.UserName}:{this.Password})");
            }

            return dbg.ToString();
        }
    }
}
