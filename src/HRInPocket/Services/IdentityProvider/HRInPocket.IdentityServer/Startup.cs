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

using Serilog;

namespace HRInPocket.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        readonly ILogger _Logger = Log.Logger;

        public Startup(IConfiguration configuration) => _Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var migration_assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //Конфигурация пользователей
            #region IdentityUsers
            services.AddDbContext<UsersDbContext>(config => config.UseSqlServer(
                    _Configuration.GetConnectionString("UsersDbConnectionString"),
                    sql => sql.MigrationsAssembly(migration_assembly)))
                  .AddIdentity<ApplicationUser, IdentityRole<Guid>>(
                   options =>
                   {
                       _Logger.Debug($"Configure password settings");
                       options.Password.RequireDigit = false;
                       options.Password.RequireLowercase = false;
                       options.Password.RequireNonAlphanumeric = false;
                       options.Password.RequireUppercase = false;
                       options.Password.RequiredLength = 6;
                   })
                  .AddEntityFrameworkStores<UsersDbContext>();
            #endregion

            //Конфигурация IdentityServer4
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

            //Конфигирация для подключения клиентов к серверу
            #region OpenId Connect
            services.AddAuthentication(opt =>
                {
                    opt.DefaultScheme = "Cookies"; // все храним в куках
                    opt.DefaultChallengeScheme = "oidc"; // протокол аутентификации - OpenId Connect
                })
               .AddCookie("Cookies", opt =>
                {
                    opt.AccessDeniedPath = "/Account/AccessDenied"; // указать, если действие в другом контроллере
                })
               
            #region Аутентификация через соцсети
           //ВКонтакте
           .AddVkontakte(config =>
            {
                //это идентификатор нашего сервера, зарегистрированный в VK
                config.ClientId = _Configuration["Authentication:VKontakte:ServiceApiKey"];
                //это секретный ключ, предоставленный сервером аутентификации VK для нашего сервера
                config.ClientSecret = _Configuration["Authentication:VKontakte:ServiceApiSecret"];
            })

            //Facebook
           .AddFacebook(config =>
            {
                config.AppId = _Configuration["Authentication:Facebook:ServiceApiKey"];
                config.AppSecret = _Configuration["Authentication:Facebook:ServiceApiSecret"];
                config.Scope.Add("email");
            })

            //Google
           .AddGoogle(config =>
            {
                config.ClientId = _Configuration["Authentication:Google:ServiceApiKey"];
                config.ClientSecret = _Configuration["Authentication:Google:ServiceApiSecret"];
                config.Scope.Add("email");
            });
            //... и т.д.
            #endregion

            //Конфигурация авторизации
            services.AddAuthorization(AuthOpt =>
            {
                AuthOpt.AddPolicy("CanCreateAndModifyData", PolicyBuilder => // добавляем политику безопасности на чтение и запись данных
                {
                    //чтобы получить этот доступ юзер должен:
                    PolicyBuilder.RequireAuthenticatedUser(); // быть аутентифицированным
                    PolicyBuilder.RequireClaim("position", "Administrator"); // иметь роль Администратора
                    PolicyBuilder.RequireClaim("country", "Russia"); // иметь клайм страны - Россия
                });
            });
            #endregion

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer(); // подключаем IdentityServer Authentication и Authorization не нужны!

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
