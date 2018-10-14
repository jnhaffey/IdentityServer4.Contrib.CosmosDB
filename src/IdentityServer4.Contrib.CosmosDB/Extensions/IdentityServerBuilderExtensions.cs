using System;
using IdentityServer4.Contrib.CosmosDB.Configuration;
using IdentityServer4.Contrib.CosmosDB.DbContext;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Contrib.CosmosDB.Options;
using IdentityServer4.Contrib.CosmosDB.Services;
using IdentityServer4.Contrib.CosmosDB.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4.Contrib.CosmosDB.Extensions
{
    /// <summary>
    ///     Extension methods to setup and configure IdentityServerBuilder
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        ///     Add Configuration Store
        /// </summary>
        /// <param name="builder">The IIdentity Server Builder</param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder, Action<CosmosDbConfiguration> setupAction)
        {
            builder.Services.Configure(setupAction);

            return builder.AddConfigurationStore();
        }

        /// <summary>
        ///     Add Configuration Store
        /// </summary>
        /// <param name="builder">The IIdentity Server Builder</param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<CosmosDbConfiguration>(configuration);

            return builder.AddConfigurationStore();
        }

        /// <summary>
        ///     Add Operational Store
        /// </summary>
        /// <param name="builder">The IIdentity Server Builder</param>
        /// <param name="setupAction"></param>
        /// <param name="tokenCleanUpOptions"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            Action<CosmosDbConfiguration> setupAction,
            Action<TokenCleanupOptions> tokenCleanUpOptions = null)
        {
            builder.Services.Configure(setupAction);

            return builder.AddOperationalStore(tokenCleanUpOptions);
        }

        /// <summary>
        ///     Add Operational Store
        /// </summary>
        /// <param name="builder">The IIdentity Server Builder</param>
        /// <param name="configuration"></param>
        /// <param name="tokenCleanUpOptions"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            IConfiguration configuration,
            Action<TokenCleanupOptions> tokenCleanUpOptions = null)
        {
            builder.Services.Configure<CosmosDbConfiguration>(configuration);

            return builder.AddOperationalStore(tokenCleanUpOptions);
        }

        /// <summary>
        ///     Use Identity Server with CosmosDb Token Cleanup.
        /// </summary>
        /// <param name="app">The Application Builder</param>
        /// <param name="applicationLifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIdentityServerCosmosDbTokenCleanup(this IApplicationBuilder app,
            IApplicationLifetime applicationLifetime)
        {
            var tokenCleanup = app.ApplicationServices.GetService<TokenCleanup>();
            if (tokenCleanup == null)
                throw new InvalidOperationException("AddOperationalStore must be called on the service collection.");
            applicationLifetime.ApplicationStarted.Register(tokenCleanup.Start);
            applicationLifetime.ApplicationStopping.Register(tokenCleanup.Stop);

            return app;
        }

        private static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IConfigurationDbContext, ConfigurationDbContext>();
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyService>();

            return builder;
        }

        private static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder,
            Action<TokenCleanupOptions> tokenCleanUpOptions = null)
        {
            builder.Services.AddScoped<IPersistedGrantDbContext, PersistedGrantDbContext>();

            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            var tokenCleanupOptions = new TokenCleanupOptions();
            tokenCleanUpOptions?.Invoke(tokenCleanupOptions);
            builder.Services.AddSingleton(tokenCleanupOptions);
            builder.Services.AddSingleton<TokenCleanup>();

            return builder;
        }
    }
}