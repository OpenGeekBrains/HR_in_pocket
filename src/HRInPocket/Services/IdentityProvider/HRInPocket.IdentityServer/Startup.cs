using System;
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

            #region InMemoryConfig
            services.AddIdentityServer() // ��������� � ������� IdentityServer
               .AddInMemoryIdentityResources(DefaultConfig.GetIdentityResources()) // ��������� ��������� �������� �������������
               .AddTestUsers(DefaultConfig.GetUsers()) // ��������� �������������
               .AddInMemoryClients(DefaultConfig.GetClients()) // ��������� ������������ ��������
               .AddInMemoryApiResources(DefaultConfig.GetApiResources()) // ��������� API ��������
               .AddInMemoryApiScopes(DefaultConfig.GetApiScopes()) // ��������� ������ (��, ������ � ���� ����� ����������������)
               .AddDeveloperSigningCredential(); // ������ �� ����� ����������. ��� ������������� ��������� �������� ����������
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer(); // ���������� IdentityServer
        }
    }
}
