using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client CORS Origins.
    /// </summary>
    public class ClientCorsOrigin
    {
        /// <summary>
        ///     Origin Value.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }
    }
}