using System;
using HRInPocket.DAL.Data;
using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Entities.Users;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;
using HRInPocket.Services.Repositories;
using HRInPocket.Services.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket
{
    public static class DIConfiguration
    {
        public static IServiceCollection AddDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<TestDbInitializer>();

            services.AddIdentity();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
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

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                //.AddScoped<IDataRepository<Resume>, DataRepository<Resume>>()
                //.AddScoped<ICompanyService, CompanyService>()
                //.AddScoped<IResumeService, ResumeService>()
                //.AddScoped<ITargetTaskService, TargetTaskService>()
                //.AddScoped<IVacancyService, VacancyService>()

                //.AddScoped<IShoppingService, ShoppingService>()
                ;

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();

            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }

        
    }
}