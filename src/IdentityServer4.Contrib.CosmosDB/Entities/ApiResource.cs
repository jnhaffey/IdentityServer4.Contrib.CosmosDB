using System.Collections.Generic;
using IdentityServer4.Contrib.CosmosDB.Abstracts;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     Models a web API resource.
    /// </summary>
    public class ApiResource : ResourceBase
    {
        /// <summary>
        ///     The API secret is used for the introspection endpoint. The API can authenticate with introspection using the API
        ///     name and secret.
        /// </summary>
        [JsonProperty("secrets")]
        public List<ApiSecret> Secrets { get; set; }

        /// <summary>
        ///     An API must have at least one scope. Each scope can have different settings.
        /// </summary>
        [JsonProperty("scopes")]
        public List<ApiScope> Scopes { get; set; }
    }
}