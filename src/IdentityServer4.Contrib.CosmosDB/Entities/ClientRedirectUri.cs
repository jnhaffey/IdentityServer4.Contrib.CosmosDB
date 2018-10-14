using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Redirect URI
    /// </summary>
    public class ClientRedirectUri
    {
        /// <summary>
        ///     Allowed URI to return tokens or authorization codes to
        /// </summary>
        [JsonProperty("redirectUri")]
        public string RedirectUri { get; set; }
    }
}