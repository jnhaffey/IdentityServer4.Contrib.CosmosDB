using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Abstracts
{
    /// <summary>
    ///     Base class used for all Entity (Document) Models.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        ///     Unique Id used to identify the document.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}