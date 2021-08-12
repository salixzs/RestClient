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
        /// NOTE: Should use other means of supplying this value as it is not constant.
        /// </summary>
        Bearer = 2,
    }
}
