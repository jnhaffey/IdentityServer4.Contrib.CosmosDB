# IdentityServer4.Contrib.CosmosDb

## Build & Release Statuses

|Branch|Build Status|Release Status|NuGet Package|
|:-|:-|:-|:-|
|Master|[![Build status](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_apis/build/status/IdentityServer4.Contrib.CosmosDB-CI%20(master))](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_build/latest?definitionId=7)|[![Deployment status](https://jnhaffey.vsrm.visualstudio.com/_apis/public/Release/badge/ff075ccf-2453-4380-bbe8-04088078c290/3/3)](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_releases2?view=all)|[![NuGet](https://img.shields.io/nuget/v/IdentityServer4.Contrib.CosmosDB.svg)](https://www.nuget.org/packages/IdentityServer4.Contrib.CosmosDB/)|
|Development|[![Build status](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_apis/build/status/IdentityServer4.Contrib.CosmosDB-CI%20(development))](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_build/latest?definitionId=6)|[![Deployment status](https://jnhaffey.vsrm.visualstudio.com/_apis/public/Release/badge/ff075ccf-2453-4380-bbe8-04088078c290/1/1)](https://jnhaffey.visualstudio.com/IdentityServer4.Contrib.CosmosDB/_releases2?view=all)|[![NuGet](https://img.shields.io/nuget/vpre/IdentityServer4.Contrib.CosmosDB.svg)](https://www.nuget.org/packages/IdentityServer4.Contrib.CosmosDB/)|

CosmosDB persistence layer for IdentityServer4 based on the [IdentityServer4.Contrib.MongoDb](https://github.com/diogodamiani/IdentityServer4.Contrib.MongoDB) persistence layer.


## General Setup and Use

_appsettings.json_
```JSON
{
  "CosmosDb": {
    "EndPointUrl": "https://localhost:8081",
    "PrimaryKey": "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
  }
}
```

_Startup.cs_
```CSharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(
            options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
{
    if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
        app.UseHsts();

    app.UseIdentityServer();
    app.UseIdentityServerCosmosDbTokenCleanup(applicationLifetime);

    app.UseStaticFiles();
    app.UseHttpsRedirection();
    app.UseMvcWithDefaultRoute();
}
```