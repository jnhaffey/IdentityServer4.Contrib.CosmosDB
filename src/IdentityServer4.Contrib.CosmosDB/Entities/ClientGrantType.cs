using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Grant Type.
    /// </summary>
    public class ClientGrantType
    {
        /// <summary>
        ///     Grant Type.
        /// </summary>
        /// <value>
        ///     Possible options AuthorizationCode, Implicit, Hybrid, ResourceOwner, ClientCredentials
        /// </value>
        [JsonProperty("grantType")]
        public string GrantType { get; set; }
    }
}