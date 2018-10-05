using System.Collections.Generic;
using IdentityServer4.Contrib.CosmosDB.Abstracts;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    public class IdentityResource : EntityBase
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<IdentityClaim> UserClaims { get; set; }
    }
}