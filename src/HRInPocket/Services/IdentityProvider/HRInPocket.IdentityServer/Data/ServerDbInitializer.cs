using System;
using System.Linq;
using HRInPocket.IdentityServer.InMemoryConfig;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HRInPocket.IdentityServer.Data
{
    public static class ServerDbInitializer
    {
        public static void Init(IServiceProvider ScopeServiceProvider)
        {
            ScopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            var Configuration = ScopeServiceProvider.GetRequiredService<IConfiguration>();
            var context = ScopeServiceProvider.GetRequiredService<ConfigurationDbContext>();
            var clients = new DefaultConfig(Configuration).GetClients();
            var logger = Log.Logger;
            var flag = false;
            context.Database.Migrate();

            logger.Debug("Checking client settings...");
            if (!context.Clients.Any())
            {
                if (!flag) flag = true;
                foreach (var client in clients)
                {
                    context.Clients.Add(client.ToEntity());
                    logger.Debug($"Added client => Client name: \"{client.ClientName}\", Client Id: {client.ClientId}, Uri: {client.ClientUri}");
                }
                context.SaveChanges();
                logger.Debug("Adding clients completed");
            }
            else logger.Debug("Client settings checked");

            logger.Debug("Checking identity resources...");
            if (!context.IdentityResources.Any())
            {
                if (!flag) flag = true;
                foreach (var resource in DefaultConfig.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                    logger.Debug($"Added identity resource => Resource name: {resource.Name}, Resource display name: {resource.DisplayName}");
                }
                context.SaveChanges();
                logger.Debug("Adding identity resources  completed");
            }
            else logger.Debug("Identity resources checked");

            logger.Debug("Checking API resources...");
            if (!context.ApiResources.Any())
            {
                if (!flag) flag = true;
                foreach (var resource in DefaultConfig.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                    logger.Debug($"Added API resource => Resource name: {resource.Name}, Resource display name: {resource.DisplayName}");
                }
                context.SaveChanges();
            }
            else logger.Debug("Identity API checked");

            var str = flag ? "updated" : "checked";
            logger.Debug($"The server settings database has been {str}");
        }
    }
}
