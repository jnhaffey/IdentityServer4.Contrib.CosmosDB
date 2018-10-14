using System.Collections.Generic;
using IdentityServer4.Contrib.CosmosDB.Entities;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Abstracts
{
    /// <inheritdoc />
    /// <summary>
    ///     Base class used for Resource Models.
    /// </summary>
    public abstract class ResourceBase : EntityBase
    {
        /// <summary>
        ///     Indicates if this resource is enabled. Defaults to true.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        ///     The unique name of the resource.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Display name of the resource.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Description of the resource.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     List of associated user claims that should be included when this resource is requested.
        /// </summary>
        [JsonProperty("userClaims")]
        public List<ApiResourceClaim> UserClaims { get; set; }
    }
}