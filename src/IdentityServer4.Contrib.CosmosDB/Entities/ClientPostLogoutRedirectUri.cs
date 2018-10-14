using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Post Logout Redirect URI.
    /// </summary>
    public class ClientPostLogoutRedirectUri
    {
        /// <summary>
        ///     URI Value.
        /// </summary>
        [JsonProperty("postLogoutRedirectUri")]
        public string PostLogoutRedirectUri { get; set; }
    }
}