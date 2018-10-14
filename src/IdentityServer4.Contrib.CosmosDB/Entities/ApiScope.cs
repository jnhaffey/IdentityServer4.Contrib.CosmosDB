using System.Collections.Generic;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <summary>
    ///     Models access to an API resource
    /// </summary>
    public class ApiScope
    {
        /// <summary>
        ///     Name of the scope. This is the value a client will use to request the scope.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Display name. This value will be used e.g. on the consent screen.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Description. This value will be used e.g. on the consent screen.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        ///     Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes.
        ///     Defaults to false.
        /// </summary>
        [JsonProperty("emphasize")]
        public bool Emphasize { get; set; }

        /// <summary>
        ///     Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("showInDiscoveryDocument")]
        public bool ShowInDiscoveryDocument { get; set; } = true;

        /// <summary>
        ///     List of user-claim types that should be included in the access token.
        /// </summary>
        [JsonProperty("userClaims")]
        public List<ApiScopeClaim> UserClaims { get; set; }
    }
}