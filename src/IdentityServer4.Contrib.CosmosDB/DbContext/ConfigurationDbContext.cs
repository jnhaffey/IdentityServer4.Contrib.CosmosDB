using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Abstracts;
using IdentityServer4.Contrib.CosmosDB.Configuration;
using IdentityServer4.Contrib.CosmosDB.Entities;
using IdentityServer4.Contrib.CosmosDB.Extensions;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer4.Contrib.CosmosDB.DbContext
{
    /// <inheritdoc cref="CosmosDbContextBase" />
    /// <summary>
    ///     Configuration DbContext Class.
    /// </summary>
    public class ConfigurationDbContext : CosmosDbContextBase, IConfigurationDbContext
    {
        private DocumentCollection _apiResources;
        private Uri _apiResourcesUri;
        private DocumentCollection _clients;
        private Uri _clientUri;
        private DocumentCollection _identityResources;
        private Uri _identityResourcesUri;

        /// <summary>
        ///     Create an instance of the ConfigurationDbContext Class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="databaseName"></param>
        /// <param name="connectionPolicy"></param>
        /// <param name="logger"></param>
        public ConfigurationDbContext(IOptions<CosmosDbConfiguration> settings,
            string databaseName = Constants.DatabaseName,
            ConnectionPolicy connectionPolicy = null,
            ILogger<ConfigurationDbContext> logger = null)
            : base(settings, databaseName, connectionPolicy, logger)
        {
            Guard.ForNullOrDefault(settings.Value, nameof(settings));
            EnsureClientsCollectionCreated().Wait();
            EnsureIdentityResourcesCollectionCreated().Wait();
            EnsureApiResourcesCollectionCreated().Wait();
        }

        /// <summary>
        ///     Add a new Client.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddClient(Client entity)
        {
            await DocumentClient.CreateDocumentAsync(_clientUri, entity);
        }

        /// <summary>
        ///     Add a new Identity Resource.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddIdentityResource(IdentityResource entity)
        {
            await DocumentClient.CreateDocumentAsync(_identityResourcesUri, entity);
        }

        /// <summary>
        ///     Add a new API Resource.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddApiResource(ApiResource entity)
        {
            await DocumentClient.CreateDocumentAsync(_apiResourcesUri, entity);
        }

        /// <summary>
        ///     Queryable Identity Resources.
        /// </summary>
        public IQueryable<IdentityResource> IdentityResources(string partitionKey = "")
        {
            return string.IsNullOrWhiteSpace(partitionKey)
                ? DocumentClient.CreateDocumentQuery<IdentityResource>(_identityResourcesUri,
                    new FeedOptions {EnableCrossPartitionQuery = true})
                : DocumentClient.CreateDocumentQuery<IdentityResource>(_identityResourcesUri,
                    new FeedOptions {PartitionKey = new PartitionKey(partitionKey)});
        }

        /// <summary>
        ///     Queryable API Resources.
        /// </summary>
        public IQueryable<ApiResource> ApiResources(string partitionKey = "")
        {
            return string.IsNullOrWhiteSpace(partitionKey)
                ? DocumentClient.CreateDocumentQuery<ApiResource>(_apiResourcesUri,
                    new FeedOptions {EnableCrossPartitionQuery = true})
                : DocumentClient.CreateDocumentQuery<ApiResource>(_apiResourcesUri,
                    new FeedOptions {PartitionKey = new PartitionKey(partitionKey)});
        }

        /// <summary>
        ///     Queryable Clients.
        /// </summary>
        public IQueryable<Client> Clients(string partitionKey = "")
        {
            return string.IsNullOrWhiteSpace(partitionKey)
                ? DocumentClient.CreateDocumentQuery<Client>(_clientUri,
                    new FeedOptions {EnableCrossPartitionQuery = true})
                : DocumentClient.CreateDocumentQuery<Client>(_clientUri,
                    new FeedOptions {PartitionKey = new PartitionKey(partitionKey)});
        }

        private async Task EnsureClientsCollectionCreated()
        {
            _clientUri = UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.Client);
            Logger?.LogDebug($"Clients URI: {_clientUri}");

            var partitionKeyDefinition = new PartitionKeyDefinition
                {Paths = {Constants.CollectionPartitionKeys.Client}};
            Logger?.LogDebug($"Client Partition Key: {partitionKeyDefinition}");

            var indexingPolicy = new IndexingPolicy
            {
                Automatic = true,
                IndexingMode = IndexingMode.Consistent
            };
            Logger?.LogDebug($"Clients Indexing Policy: {indexingPolicy}");

            _clients = new DocumentCollection
            {
                Id = Constants.CollectionNames.Client,
                PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy
            };
            Logger?.LogDebug($"Clients Collection: {_clients}");

            Logger?.LogDebug($"Ensure Clients (ID:{_clients.Id}) collection exists...");
            var clientsRequestOptions = new RequestOptions
            {
                OfferThroughput = GetRUsFor(CollectionName.Clients)
            };
            Logger?.LogDebug($"Clients Request Options: {clientsRequestOptions}");
            var clientResults = await DocumentClient.CreateDocumentCollectionIfNotExistsAsync(DatabaseUri,
                _clients, clientsRequestOptions);
            Logger?.LogDebug($"{_clients.Id} Creation Results: {clientResults.StatusCode}");
            if (clientResults.StatusCode.EqualsOne(HttpStatusCode.Created, HttpStatusCode.OK))
                _clients = clientResults.Resource;
        }

        private async Task EnsureIdentityResourcesCollectionCreated()
        {
            _identityResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.IdentityResource);
            Logger?.LogDebug($"Identity Resources URI: {_identityResourcesUri}");

            var partitionKeyDefinition = new PartitionKeyDefinition
                {Paths = {Constants.CollectionPartitionKeys.IdentityResource}};
            Logger?.LogDebug($"Identity Resources Partition Key: {partitionKeyDefinition}");

            var indexingPolicy = new IndexingPolicy
            {
                Automatic = true,
                IndexingMode = IndexingMode.Consistent
            };
            Logger?.LogDebug($"Identity Resources Indexing Policy: {indexingPolicy}");

            _identityResources = new DocumentCollection
            {
                Id = Constants.CollectionNames.IdentityResource,
                PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy
            };
            Logger?.LogDebug($"Identity Resources Collection: {_identityResources}");

            Logger?.LogDebug($"Ensure Identity Resources (ID:{_identityResources.Id}) collection exists...");
            var identityResourcesRequestOptions = new RequestOptions
            {
                OfferThroughput = GetRUsFor(CollectionName.IdentityResources)
            };
            Logger?.LogDebug($"Identity Resources Request Options: {identityResourcesRequestOptions}");
            var identityResourceResults = await DocumentClient.CreateDocumentCollectionIfNotExistsAsync(DatabaseUri,
                _identityResources, identityResourcesRequestOptions);
            Logger?.LogDebug($"{_identityResources.Id} Creation Results: {identityResourceResults.StatusCode}");
            if (identityResourceResults.StatusCode.EqualsOne(HttpStatusCode.Created, HttpStatusCode.OK))
                _identityResources = identityResourceResults.Resource;
        }

        private async Task EnsureApiResourcesCollectionCreated()
        {
            _apiResourcesUri =
                UriFactory.CreateDocumentCollectionUri(Database.Id, Constants.CollectionNames.ApiResource);
            Logger?.LogDebug($"API Resources URI: {_apiResourcesUri}");

            var partitionKeyDefinition = new PartitionKeyDefinition
                {Paths = {Constants.CollectionPartitionKeys.ApiResource}};
            Logger?.LogDebug($"API Resources Partition Key: {partitionKeyDefinition}");

            var indexingPolicy = new IndexingPolicy
            {
                Automatic = true,
                IndexingMode = IndexingMode.Consistent
            };
            Logger?.LogDebug($"API Resources Index Policy: {indexingPolicy}");

            _apiResources = new DocumentCollection
            {
                Id = Constants.CollectionNames.ApiResource,
                PartitionKey = partitionKeyDefinition,
                IndexingPolicy = indexingPolicy
            };
            Logger?.LogDebug($"API Resources Collection: {_apiResources}");

            var apiResourcesRequestOptions = new RequestOptions
            {
                OfferThroughput = GetRUsFor(CollectionName.ApiResources)
            };
            Logger?.LogDebug($"API Resources Request Options: {apiResourcesRequestOptions}");

            Logger?.LogDebug($"Ensure API Resources (ID:{_apiResources.Id}) collection exists...");
            var apiResourceResults = await DocumentClient.CreateDocumentCollectionIfNotExistsAsync(DatabaseUri,
                _apiResources, apiResourcesRequestOptions);
            Logger?.LogDebug($"{_apiResources.Id} Creation Results: {apiResourceResults.StatusCode}");
            if (apiResourceResults.StatusCode.EqualsOne(HttpStatusCode.Created, HttpStatusCode.OK))
                _apiResources = apiResourceResults.Resource;
        }
    }
}