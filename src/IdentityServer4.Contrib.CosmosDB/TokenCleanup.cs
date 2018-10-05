using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Contrib.CosmosDB.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer4.Contrib.CosmosDB
{
    public class TokenCleanup
    {
        private readonly TimeSpan _interval;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private CancellationTokenSource _source;

        public TokenCleanup(IServiceProvider serviceProvider, ILogger logger, TokenCleanupOptions options)
        {
            Guard.ForNull(serviceProvider, nameof(serviceProvider));
            Guard.ForNull(logger, nameof(logger));
            Guard.ForNull(options, nameof(options));
            Guard.ForValueLessThan(options.Interval, 1, nameof(options.Interval));

            _serviceProvider = serviceProvider;
            _logger = logger;
            _interval = TimeSpan.FromSeconds(options.Interval);
        }

        public void Start()
        {
            if (_source != null) throw new InvalidOperationException($"Already started, call `{nameof(Stop)}` first.");

            _logger.LogDebug("Starting token cleanup.");

            _source = new CancellationTokenSource();
            Task.Factory.StartNew(() => Start(_source.Token));
        }

        public void Stop()
        {
            if (_source == null) throw new InvalidOperationException($"Not started, call `{nameof(Start)}` first.");

            _logger.LogDebug("Stopping token cleanup.");
            _source.Cancel();
            _source = null;
        }

        private async Task Start(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested");
                    break;
                }

                try
                {
                    await Task.Delay(_interval, cancellationToken);
                }
                catch
                {
                    _logger.LogDebug("Task.Delay exception. exiting.");
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested");
                    break;
                }

                await ClearTokens();
            }
        }

        private async Task ClearTokens()
        {
            try
            {
                _logger.LogTrace("Querying for tokens to clear");

                using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<IPersistedGrantDbContext>())
                    {
                        var expired = context.PersistedGrants.Where(x => x.Expiration < DateTime.UtcNow).ToArray();

                        _logger.LogDebug("Clearing {tokenCount} tokens", expired.Length);

                        if (expired.Length > 0) await context.RemoveExpired();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception cleaning tokens {exception}", ex.Message);
            }
        }
    }
}