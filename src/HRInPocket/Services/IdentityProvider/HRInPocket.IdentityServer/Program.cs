using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using HRInPocket.IdentityServer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HRInPocket.IdentityServer
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();
            try
            {
                Log.Information("Application initialize...");
                var host = CreateHostBuilder(args).Build();
                Log.Debug("host created");

                using (var scope = host.Services.CreateScope())
                {
                    Log.Debug("Initialize server settings Db");
                    ServerDbInitializer.Init(scope.ServiceProvider);

                    Log.Debug("Initialize users Db");
                    UsersDbInitializer.Init(scope.ServiceProvider);
                }

                Log.Information("Application run");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
           .CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(WebBuilder => WebBuilder
                   .UseStartup<Startup>()
                   .ConfigureLogging(log => log.AddSerilog(Log.Logger)))
           .UseSerilog();
    }
}
