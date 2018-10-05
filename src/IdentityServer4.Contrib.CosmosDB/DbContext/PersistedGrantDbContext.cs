using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Configuration;
using IdentityServer4.Contrib.CosmosDB.Entities;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer4.Contrib.CosmosDB.DbContext
{
    public class PersistedGrantDbContext : CosmosDbContextBase, IPersistedGrantDbContext
    {
        private DocumentCollection _persistedGrants;
        private Uri _persistedGrantsUri;

        public PersistedGrantDbContext(IOptions<CosmosDbConfiguration> settings, string databaseName,
            ConnectionPolicy connectionPolicy = null, ILogger logger = null) : base(settings, databaseName,
            connectionPolicy, logger)
        {
            Guard.ForNullOrDefault(settings.Value, nameof(settings));
            SetupPersistedGrants();
        }

        public IQueryable<PersistedGrant> PersistedGrants =>
            DocumentClient.CreateDocumentQuery<PersistedGrant>(_persistedGrantsUri);

        public async Task Add(PersistedGrant entity)
        {
            await DocumentClient.CreateDocumentAsync(_persistedGrantsUri, entity);
        }

        public async Task Remove(Expression<Func<PersistedGrant, bool>> filter)
        {
            foreach (var persistedGrant in PersistedGrants.Where(filter)) await Remove(persistedGrant);
        }

        public async Task RemoveExpired()
        {
            foreach (var expired in PersistedGrants.Where(x => x.Expiration < DateTime.UtcNow)) await Remove(expired);
        }

        public async Task Update(PersistedGrant entity)
        {
            var documentUrl = UriFactory.CreateDocumentUri(Database.Id, _persistedGrants.Id, entity.ClientId);
            await DocumentClient.ReplaceDocumentAsync(documentUrl, entity);
        }

        public async Task Update(Expression<Func<PersistedGrant, bool>> filter, PersistedGrant entity)
        {
            // TODO : This looks like its a MongoDb specific thing.  This is an attempt to match it.
            // await _persistedGrants.ReplaceOneAsync(filter, entity);
            await DocumentClient.UpsertDocumentAsync(_persistedGrantsUri, entity);
        }

        public async Task Remove(PersistedGrant entity)
        {
            var documentUrl = UriFactory.CreateDocumentUri(Database.Id, _persistedGrants.Id, entity.ClientId);
            await DocumentClient.DeleteDocumentAsync(documentUrl);
        }

        private void SetupPersistedGrants()
        {
            _persistedGrantsUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.PersistedGrant);

            var partitionKeyDefinition = new PartitionKeyDefinition();
            partitionKeyDefinition.Paths.Add(Constants.PartitionKey);

            var indexingPolicy = new IndexingPolicy {Automatic = true, IndexingMode = IndexingMode.Consistent};
            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/Key",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/SubjectId",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            var uniqueKeyPolicy = new UniqueKeyPolicy();
            uniqueKeyPolicy.UniqueKeys.Add(new UniqueKey
            {
                Paths =
                {
                    "/ClientId",
                    "/SubjectId",
                    "/Type"
                }
            });

            var persistedGrants = new DocumentCollection
            {
                Id = Constants.CollectionNames.PersistedGrant,
                PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy,
                UniqueKeyPolicy = uniqueKeyPolicy
            };

            _persistedGrants = DocumentClient
                .CreateDocumentCollectionIfNotExistsAsync(_persistedGrantsUri, persistedGrants).Result;
        }
    }
}