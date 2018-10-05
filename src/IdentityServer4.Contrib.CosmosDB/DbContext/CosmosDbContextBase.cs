using System;
using IdentityServer4.Contrib.CosmosDB.Configuration;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer4.Contrib.CosmosDB.DbContext
{
    public abstract class CosmosDbContextBase : IDisposable
    {
        private readonly ILogger _logger;

        protected CosmosDbContextBase(IOptions<CosmosDbConfiguration> settings,
            string databaseName, ConnectionPolicy connectionPolicy = null, ILogger logger = null)
        {
            Guard.ForNullOrDefault(settings.Value, nameof(settings));
            Guard.ForNullOrDefault(settings.Value.EndPointUrl, nameof(settings.Value.EndPointUrl));
            Guard.ForNullOrDefault(settings.Value.PrimaryKey, nameof(settings.Value.PrimaryKey));
            _logger = logger;

            var serviceEndPoint = new Uri(settings.Value.EndPointUrl);
            DocumentClient = new DocumentClient(serviceEndPoint, settings.Value.PrimaryKey,
                connectionPolicy ?? ConnectionPolicy.Default);

            DatabaseUri = UriFactory.CreateDatabaseUri(databaseName);
            Database = new Database {Id = databaseName};
            Database = DocumentClient.CreateDatabaseIfNotExistsAsync(Database).Result;
        }

        protected DocumentClient DocumentClient { get; }
        protected Database Database { get; }
        protected Uri DatabaseUri { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO : Dispose of any resources
            }
        }

        ~CosmosDbContextBase()
        {
            Dispose(false);
        }
    }
}