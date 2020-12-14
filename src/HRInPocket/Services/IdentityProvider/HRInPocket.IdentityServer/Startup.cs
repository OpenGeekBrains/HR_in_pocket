using System;
using System.Reflection;
using HRInPocket.IdentityServer.Data;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRInPocket.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration configuration) => _Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var migration_assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            #region IdentityUsers
            services.AddDbContext<UsersDbContext>(
                   config => config.UseSqlServer(_Configuration.GetConnectionString("UsersDbConnectionString")))
                  .AddIdentity<ApplicationUser, IdentityRole<Guid>>(
                   options =>
                   {
                       options.Password.RequireDigit = false;
                       options.Password.RequireLowercase = false;
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireUppercase = false;
                       options.Password.RequiredLength = 6;
                   })
                  .AddEntityFrameworkStores<UsersDbContext>();
            #endregion

            #region IdentityServer
            services.AddIdentityServer() // добавляем в систему IdentityServer
                   .AddInMemoryIdentityResources(DefaultConfig.GetIdentityResources()) // настройки доступных ресурсов пользователей
                   .AddAspNetIdentity<ApplicationUser>() // пользователи системы Microsoft Identity User
                   .AddInMemoryClients(DefaultConfig.GetClients()) // настройки подключаемых клиентов
                   .AddInMemoryApiResources(DefaultConfig.GetApiResources()) // настройки API ресурсов
                   .AddInMemoryApiScopes(DefaultConfig.GetApiScopes()) // настройки скопов (то, доступ к чему будет контролироваться)
                   .AddDeveloperSigningCredential() // только на время разработки. При развертывании необходим реальный сертификат
                   .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = c => c.UseSqlServer(
                            _Configuration.GetConnectionString("ServerConfigDbConnectionString"),
                            sql => sql.MigrationsAssembly(migration_assembly));
                    })
                   .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = o => o.UseSqlServer(_Configuration.GetConnectionString("ServerConfigDbConnectionString"),
                            sql => sql.MigrationsAssembly(migration_assembly));
                    }); 
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer(); // подключаем IdentityServer
        }
    }
}
