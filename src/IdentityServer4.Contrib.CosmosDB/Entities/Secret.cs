using System;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Models a client secret with identifier and expiration
    /// </summary>
    public class Secret
    {
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets the expiration.
        /// </summary>
        /// <value>
        ///     The expiration.
        /// </value>
        [JsonProperty("expiration")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        ///     Gets or sets the type of the client secret.
        /// </summary>
        /// <value>
        ///     The type of the client secret.
        /// </value>
        /// <remarks>Default value is `SharedSecret`.</remarks>
        [JsonProperty("type")]
        public string Type { get; set; } = IdentityServerConstants.SecretTypes.SharedSecret;
    }
}