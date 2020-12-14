using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.InMemoryConfig;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.IdentityServer.Data
{
    public static class ServerDbInitializer
    {
        public static void Init(IServiceProvider ScopeServiceProvider)
        {
            ScopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = ScopeServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in DefaultConfig.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in DefaultConfig.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in DefaultConfig.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
