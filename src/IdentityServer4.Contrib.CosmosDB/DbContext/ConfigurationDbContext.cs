using System;
using System.Linq;
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
    public class ConfigurationDbContext : CosmosDbContextBase, IConfigurationDbContext
    {
        private DocumentCollection _apiResources;
        private Uri _apiResourcesUri;
        private DocumentCollection _clients;
        private Uri _clientUri;
        private DocumentCollection _identityResources;
        private Uri _identityResourcesUri;

        public ConfigurationDbContext(IOptions<CosmosDbConfiguration> settings,
            string databaseName = Constants.DatabaseName,
            ConnectionPolicy connectionPolicy = null,
            ILogger logger = null)
            : base(settings, databaseName, connectionPolicy, logger)
        {
            Guard.ForNullOrDefault(settings.Value, nameof(settings));
            SetupClientsOptions();
            SetupIdentityResourceOptions();
            SetupApiResourceOptions();
        }

        public IQueryable<Client> Clients => DocumentClient.CreateDocumentQuery<Client>(_clientUri);

        public IQueryable<IdentityResource> IdentityResources =>
            DocumentClient.CreateDocumentQuery<IdentityResource>(_identityResourcesUri);

        public IQueryable<ApiResource> ApiResources =>
            DocumentClient.CreateDocumentQuery<ApiResource>(_apiResourcesUri);

        public async Task AddClient(Client entity)
        {
            await DocumentClient.CreateDocumentAsync(_clientUri, entity);
        }

        public async Task AddIdentityResource(IdentityResource entity)
        {
            await DocumentClient.CreateDocumentAsync(_identityResourcesUri, entity);
        }

        public async Task AddApiResource(ApiResource entity)
        {
            await DocumentClient.CreateDocumentAsync(_apiResourcesUri, entity);
        }

        private void SetupClientsOptions()
        {
            _clientUri = UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.Client);

            var partitionKeyDefinition = new PartitionKeyDefinition();
            partitionKeyDefinition.Paths.Add(Constants.PartitionKey);

            var indexingPolicy = new IndexingPolicy {Automatic = true, IndexingMode = IndexingMode.Consistent};
            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/ClientId",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            var clients = new DocumentCollection
            {
                Id = Constants.CollectionNames.Client, PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy
            };

            _clients = DocumentClient.CreateDocumentCollectionIfNotExistsAsync(_clientUri, clients).Result;
        }

        private void SetupIdentityResourceOptions()
        {
            _identityResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.IdentityResource);

            var partitionKeyDefinition = new PartitionKeyDefinition();
            partitionKeyDefinition.Paths.Add(Constants.PartitionKey);

            var indexingPolicy = new IndexingPolicy {Automatic = true, IndexingMode = IndexingMode.Consistent};
            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/Name",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            var identityResources = new DocumentCollection
            {
                Id = Constants.CollectionNames.IdentityResource, PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy
            };

            _identityResources = DocumentClient
                .CreateDocumentCollectionIfNotExistsAsync(_identityResourcesUri, identityResources).Result;
        }

        private void SetupApiResourceOptions()
        {
            _apiResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.ApiResource);

            var partitionKeyDefinition = new PartitionKeyDefinition();
            partitionKeyDefinition.Paths.Add(Constants.PartitionKey);

            var indexingPolicy = new IndexingPolicy {Automatic = true, IndexingMode = IndexingMode.Consistent};
            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/Name",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            indexingPolicy.IncludedPaths.Add(new IncludedPath
            {
                Path = "/Scopes",
                Indexes =
                {
                    new RangeIndex(DataType.String),
                    new HashIndex(DataType.String)
                }
            });

            var apiResources = new DocumentCollection
                {Id = Constants.CollectionNames.ApiResource, PartitionKey = partitionKeyDefinition};

            _apiResources = DocumentClient.CreateDocumentCollectionIfNotExistsAsync(_apiResourcesUri, apiResources)
                .Result;
        }
    }
}