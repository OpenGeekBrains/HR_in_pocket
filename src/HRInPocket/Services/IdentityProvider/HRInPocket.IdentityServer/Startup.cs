using System;
using System.Reflection;
using HRInPocket.IdentityServer.Data;
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

            //������������ �������������
            #region IdentityUsers
            services.AddDbContext<UsersDbContext>(config =>
#if DEBUG
                config.UseInMemoryDatabase("InMemoryDb")
#else
                    config.UseSqlServer(
                    _Configuration.GetConnectionString("UsersDbConnectionString"),
                    sql => sql.MigrationsAssembly(migration_assembly))
#endif
                    
                    )
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

            //������������ IdentityServer4
            #region IdentityServer
            services.AddIdentityServer() // ��������� � ������� IdentityServer
               .AddDeveloperSigningCredential() // ������ �� ����� ����������. ��� ������������� ��������� �������� ����������
               .AddAspNetIdentity<ApplicationUser>()
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

            #region �������������� ����� �������
            services.AddAuthentication()
           //���������
           .AddVkontakte(config =>
            {
                //��� ������������� ������ �������, ������������������ � VK
                config.ClientId = _Configuration["Authentication:VKontakte:ServiceApiKey"];
                //��� ��������� ����, ��������������� �������� �������������� VK ��� ������ �������
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
            //... � �.�.
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

            app.UseIdentityServer(); // ���������� IdentityServer Authentication � Authorization �� �����!

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
