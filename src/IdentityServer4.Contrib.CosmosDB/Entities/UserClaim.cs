using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of User Claim.
    /// </summary>
    public class UserClaim
    {
        /// <summary>
        ///     Claim Id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Claim Type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}