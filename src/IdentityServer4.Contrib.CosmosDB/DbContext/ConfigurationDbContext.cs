using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Configuration;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer4.Contrib.CosmosDB.DbContext
{
    public class ConfigurationDbContext : CosmosDbContextBase, IConfigurationDbContext
    {
        private readonly Uri _apiResourcesUri;
        private readonly Uri _clientUri;
        private readonly Uri _identityResourcesUri;

        public ConfigurationDbContext(IOptions<CosmosDbConfiguration> settings,
            string databaseName = Constants.DatabaseName,
            ConnectionPolicy connectionPolicy = null,
            ILogger logger = null)
            : base(settings, databaseName, connectionPolicy, logger)
        {
            Guard.ForNullOrDefault(settings.Value, nameof(settings));

            var partitionKeyDefinition = new PartitionKeyDefinition();
            partitionKeyDefinition.Paths.Add(Constants.PartitionKey);

            _clientUri = UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.Client);
            var clients = new DocumentCollection
                {Id = Constants.CollectionNames.Client, PartitionKey = partitionKeyDefinition};
            clients = DocumentClient.CreateDocumentCollectionIfNotExistsAsync(_clientUri, clients).Result;

            _identityResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.IdentityResource);
            var identityResources = new DocumentCollection {Id = Constants.CollectionNames.IdentityResource};
            identityResources = DocumentClient
                .CreateDocumentCollectionIfNotExistsAsync(_identityResourcesUri, identityResources).Result;

            _apiResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.ApiResource);
            var apiResources = new DocumentCollection {Id = Constants.CollectionNames.ApiResource};
            apiResources = DocumentClient.CreateDocumentCollectionIfNotExistsAsync(_apiResourcesUri, apiResources)
                .Result;
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
    }
}