namespace IdentityServer4.Contrib.CosmosDB.Configuration
{
    /// <summary>
    ///     AppSettings CosmosDb Configuration Sub-Section (Collections).
    /// </summary>
    public class Collection
    {
        /// <summary>
        ///     Collection Name
        /// </summary>
        /// <value>
        ///     ApiResources
        ///     Clients
        ///     IdentityResources
        /// </value>
        public CollectionName CollectionName { get; set; }

        /// <summary>
        ///     The number of RU/sec to set for this collection.
        ///     <c>Default is 1000</c>
        /// </summary>
        public int ReserveUnits { get; set; } = 1000;
    }
}