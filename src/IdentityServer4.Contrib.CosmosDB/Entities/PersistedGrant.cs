using System;
using IdentityServer4.Contrib.CosmosDB.Abstracts;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    public class PersistedGrant : EntityBase
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}