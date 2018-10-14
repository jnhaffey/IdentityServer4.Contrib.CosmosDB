using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Claim.
    /// </summary>
    public class ClientClaim
    {
        /// <summary>
        ///     Claim Type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Claim Value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}