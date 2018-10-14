using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Host
{
    public class Program
    {
        public static string AppName = Assembly.GetEntryAssembly().GetName().Name;
        public static Version AppVersion = Assembly.GetEntryAssembly().GetName().Version;

        public static IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Console.Title = $"{AppName}-v{AppVersion}";

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.ColoredConsole(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateLogger();

            try
            {
                Log.Information($"Starting up {Console.Title}...");

                CreateWebHostBuilder(args).Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>()
            .Build();
    }
}