namespace Salix.RestClient
{
    public enum ApiAuthenticationType
    {
        /// <summary>
        /// API does not use any authentication.
        /// </summary>
        None = 0,

        /// <summary>
        /// API uses Basic authentication = username:password
        /// </summary>
        Basic = 1,

        /// <summary>
        /// API uses Bearer token authentication.
        /// </summary>
        Bearer = 2,
    }
}
