using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Salix.RestClient
{
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
        public string UserName { get; set; }

        /// <summary>
        /// Password (with <see cref="UserName"/>) for authentication when type is Basic.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Bearer token for REST service authentication with it.
        /// NOTE: Should use other means to supply/change this as it is not a constant value.
        /// </summary>
        public string BearerToken { get; set; }

        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay
        {
            get
            {
                var dbg = new StringBuilder($"Auth: {this.AuthenticationType}");
                if (this.AuthenticationType == ApiAuthenticationType.Basic)
                {
                    dbg.Append($" ({this.UserName}:{this.Password})");
                }

                if (this.AuthenticationType == ApiAuthenticationType.Bearer)
                {
                    dbg.Append($" (Token: {this.BearerToken})");
                }

                return dbg.ToString();
            }
        }
    }
}
