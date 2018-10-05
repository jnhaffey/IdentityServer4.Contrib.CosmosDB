namespace IdentityServer4.Contrib.CosmosDB
{
    public class Constants
    {
        public const string DatabaseName = "IdentityDb";
        public const string PartitionKey = "/partitionKey";

        public class CollectionNames
        {
            // Configuration
            public const string IdentityResource = "IdentityResources";
            public const string IdentityClaim = "IdentityClaims";

            public const string ApiResource = "ApiResources";
            public const string ApiSecret = "ApiSecrets";
            public const string ApiScope = "ApiScopes";
            public const string ApiClaim = "ApiClaims";
            public const string ApiScopeClaim = "ApiScopeClaims";
            
            public const string Client = "Clients";
            public const string ClientGrantType = "ClientGrantTypes";
            public const string ClientRedirectUri = "ClientRedirectUris";
            public const string ClientPostLogoutRedirectUri = "ClientPostLogoutRedirectUris";
            public const string ClientScopes = "ClientScopes";
            public const string ClientSecret = "ClientSecrets";
            public const string ClientClaim = "ClientClaims";
            public const string ClientIdPRestriction = "ClientIdPRestrictions";
            public const string ClientCorsOrigin = "ClientCorsOrigins";

            // Operational
            public const string PersistedGrant = "PersistedGrants";
        }

        public class Placeholders
        {
            public const string ParameterName = "ParameterName";
            public const string MinimumValue = "MinimumValue";
        }
    }
}