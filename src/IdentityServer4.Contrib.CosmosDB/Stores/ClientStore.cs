using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Extensions;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.CosmosDB.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IConfigurationDbContext _context;
        private readonly ILogger _logger;

        public ClientStore(IConfigurationDbContext context, ILogger<ClientStore> logger)
        {
            Guard.ForNull(context, nameof(context));
            Guard.ForNull(logger, nameof(logger));

            _context = context;
            _logger = logger;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            // TECH DEBT : CosmosDB currently does not support first FirstOrDefault
            //var client = _context.Clients(clientId).FirstOrDefault(x => x.ClientId == clientId);
            var clients = _context.Clients(clientId).ToList();

            var model = clients?.FirstOrDefault().ToModel();

            _logger.LogDebug($"{clientId} found in database: {model != null}");

            return Task.FromResult(model);
        }
    }
}