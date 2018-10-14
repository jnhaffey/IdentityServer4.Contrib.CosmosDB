using System;
using IdentityServer4.Contrib.CosmosDB.Abstracts;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     A model for a persisted grant
    /// </summary>
    public class PersistedGrant : EntityBase
    {
        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Gets the subject identifier.
        /// </summary>
        /// <value>
        ///     The subject identifier.
        /// </value>
        [JsonProperty("subjectId")]
        public string SubjectId { get; set; }

        /// <summary>
        ///     Gets the client identifier.
        /// </summary>
        /// <value>
        ///     The client identifier.
        /// </value>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the creation time.
        /// </summary>
        /// <value>
        ///     The creation time.
        /// </value>
        [JsonProperty("creationTime")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///     Gets or sets the expiration.
        /// </summary>
        /// <value>
        ///     The expiration.
        /// </value>
        [JsonProperty("expiration")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}