using System;
using System.Collections.Generic;

using AutoMapper;

using HRInPocket.DAL.Data;
using HRInPocket.Infrastructure.Profiles;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;
using HRInPocket.Services.Mapper;
using HRInPocket.Services.Repositories;
using HRInPocket.Services.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HRInPocket
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDB(Configuration);
            services.AddServices(Configuration);



            services.AddAutoMapper(
                typeof(MappingProfile),
                typeof(AccountsProfile)
                );

            services.AddSwaggerGen(setup => setup
                .SwaggerDoc("v1", new OpenApiInfo
            {
                #if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
                #endif

                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            #region Services

            //services.AddScoped<IDataRepository<T>, DataRepository<T>>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddScoped<IShoppingService, ShoppingService>();
            services.AddScoped<ITargetTaskService, TargetTaskService>();
            services.AddScoped<IVacancyService, VacancyService>();

            #endregion

            services.AddControllersWithViews();
                Title = "HR in Pocket API",
                Version = "v1"
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TestDbInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                    {
                        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "HR in Pocket API v1");
                    }
                );
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    
}