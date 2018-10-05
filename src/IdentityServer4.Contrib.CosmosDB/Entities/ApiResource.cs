using System.Collections.Generic;
using IdentityServer4.Contrib.CosmosDB.Abstracts;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    public class ApiResource : EntityBase
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ApiSecret> Secrets { get; set; }
        public List<ApiScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }
    }
}