using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Id Restriction.
    /// </summary>
    public class ClientIdPRestriction
    {
        /// <summary>
        ///     Provider Value.
        /// </summary>
        [JsonProperty("provider")]
        public string Provider { get; set; }
    }
}