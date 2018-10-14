using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Instance of Client Property (KeyValue).
    /// </summary>
    public class ClientProperty
    {
        /// <summary>
        ///     Key
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}