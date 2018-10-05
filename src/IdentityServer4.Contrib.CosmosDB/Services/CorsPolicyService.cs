using System;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.CosmosDB.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly IConfigurationDbContext _context;
        private readonly ILogger _logger;

        public CorsPolicyService(IConfigurationDbContext context, ILogger logger)
        {
            Guard.ForNull(context, nameof(context));
            Guard.ForNull(logger, nameof(logger));

            _context = context;
            _logger = logger;
        }

        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            throw new NotImplementedException();
        }
    }
}