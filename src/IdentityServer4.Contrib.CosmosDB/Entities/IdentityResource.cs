using IdentityServer4.Contrib.CosmosDB.Abstracts;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     Models a user identity resource.
    /// </summary>
    public class IdentityResource : ResourceBase
    {
        /// <summary>
        ///     Specifies whether the user can de-select the scope on the consent screen (if the consent screen wants to implement
        ///     such a feature). Defaults to false.
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        ///     Specifies whether the consent screen will emphasize this scope (if the consent screen wants to implement such a
        ///     feature).
        ///     Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        [JsonProperty("emphasize")]
        public bool Emphasize { get; set; }

        /// <summary>
        ///     Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        [JsonProperty("showInDiscoveryDocument")]
        public bool ShowInDiscoveryDocument { get; set; } = true;
    }
}