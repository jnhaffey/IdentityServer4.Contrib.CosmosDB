namespace IdentityServer4.Contrib.CosmosDB.Options
{
    public class TokenCleanupOptions
    {
        public int Interval { get; set; } = 60;
    }
}