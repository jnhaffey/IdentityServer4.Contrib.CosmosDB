using System;
using System.Linq;
using Host.Configuration;
using Host.Extensions;
using IdentityServer4.Contrib.CosmosDB.Extensions;
using IdentityServer4.Contrib.CosmosDB.Interfaces;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddConfigurationStore(Configuration.GetSection("CosmosDB"))
                .AddOperationalStore(Configuration.GetSection("CosmosDB"))
                .AddDeveloperSigningCredential()
                .AddExtensionGrantValidator<ExtensionGrantValidator>()
                .AddExtensionGrantValidator<NoSubjectExtensionGrantValidator>()
                .AddJwtBearerClientAuthentication()
                .AddAppAuthRedirectUriValidator()
                .AddTestUsers(TestUsers.Users);

            services.AddExternalIdentityProviders();

            return services.BuildServiceProvider(true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // Setup Databases
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                EnsureSeedData(serviceScope.ServiceProvider.GetService<IConfigurationDbContext>());
            }

            app.UseIdentityServer();
            app.UseIdentityServerCosmosDbTokenCleanup(applicationLifetime);

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }

        private static void EnsureSeedData(IConfigurationDbContext context)
        {
            foreach (var client in Clients.Get().ToList())
            {
                var dbRecords = context.Clients(client.ClientId).ToList();
                if (dbRecords.Count == 0) context.AddClient(client.ToEntity());
            }

            foreach (var resource in Resources.GetIdentityResources().ToList())
            {
                var dbRecords = context.IdentityResources(resource.Name).ToList();
                if (dbRecords.Count == 0) context.AddIdentityResource(resource.ToEntity());
            }

            foreach (var resource in Resources.GetApiResources().ToList())
            {
                var dbRecords = context.ApiResources(resource.Name).ToList();
                if (dbRecords.Count == 0) context.AddApiResource(resource.ToEntity());
            }
        }
    }
}