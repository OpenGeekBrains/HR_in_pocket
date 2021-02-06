using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;

using HRInPocket.IdentityServer.Data;
using HRInPocket.IdentityServer.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
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
            })
           .AddOAuth("HH", config =>
           {
               var apiHhDomain = ""; //_Configuration["HH:ApiDomain"];
               config.ClientId = ""; //_Configuration["Authentication:HH:ServiceApiKey"];
               config.ClientSecret = ""; // _Configuration["Authentication:HH:ServiceApiSecret"];
               config.AuthorizationEndpoint = "https://hh.ru/oauth/authorize";
               config.TokenEndpoint = "https://hh.ru/oauth/token";
               config.UserInformationEndpoint = $"{apiHhDomain}/me";

               config.Events = new OAuthEvents
               {
                   OnCreatingTicket = async context => 
                   {
                       //���������� ����� ����, ��� ��������� ������� ��������������� ������������.

                       var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                       request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                       //����������� ������ � ������������
                       var respone = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                       respone.EnsureSuccessStatusCode();

                       //��������������� �� ������ respone.Content.ReadAsStringAsync() ���������� � ������������
                       //���� ���� ������������ � ����� id, ������ �������������� ��� �� ����� �������
                       //����� ������� ������ ������������ � ��������������
                   }
               };
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
