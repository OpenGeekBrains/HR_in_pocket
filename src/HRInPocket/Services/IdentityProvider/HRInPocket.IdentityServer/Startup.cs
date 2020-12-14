using HRInPocket.IdentityServer.InMemoryConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRInPocket.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region InMemoryConfig
            services.AddIdentityServer() // добавляем в систему IdentityServer
               .AddInMemoryIdentityResources(DefaultConfig.GetIdentityResources()) // настройки доступных ресурсов пользователей
               .AddTestUsers(DefaultConfig.GetUsers()) // настройки пользователей
               .AddInMemoryClients(DefaultConfig.GetClients()) // настройки подключаемых клиентов
               //.AddInMemoryApiResources(DefaultConfig.GetApiResources()) // настройки API ресурсов
               //.AddInMemoryApiScopes(DefaultConfig.GetApiScopes()) // настройки скопов (то, доступ к чему будет контролироваться)
               .AddDeveloperSigningCredential(); // только на время разработки. При развертывании необходим реальный сертификат
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
