using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Repository;
using HRInPocket.Interfaces.Repository.Base;
using HRInPocket.Interfaces.Services;
using HRInPocket.Services.Repositories;
using HRInPocket.Services.Repositories.Base;
using HRInPocket.Services.Services;

using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.Services
{
    public static class Registrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
               .AddScoped<IMailSenderService, MailSenderService>()
               .AddScoped<IPaymentService, PaymentService>();

            //services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ITaskManager, TaskManager>();

            services
                .AddTransient<AuthService>()
                .AddTransient<ApplicantManagerService>()
                .AddTransient<AssignmentsManagerService>()
                .AddTransient<FeedBackService>()
                .AddTransient<NotifyService>();

            services.AddRepositories();


            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IDataRepository<Company>, DataRepository<Company>>()
                .AddScoped<IDataRepository<Resume>, DataRepository<Resume>>()
                .AddScoped<IDataRepository<TargetTask>, DataRepository<TargetTask>>()
                .AddScoped<IDataRepository<Vacancy>, DataRepository<Vacancy>>()
                .AddScoped<IDataRepository<Tarif>, DataRepository<Tarif>>()
                .AddScoped<IDataRepository<PriceItem>, DataRepository<PriceItem>>()

                //.AddScoped<ICompanyService, CompanyService>()
                //.AddScoped<IResumeService, ResumeService>()
                .AddScoped<IPaymentService, PaymentService>()
                //.AddScoped<ITargetTaskService, TargetTaskService>()
                //.AddScoped<IVacancyService, VacancyService>()

                //.AddScoped<IShoppingService, ShoppingService>()
        ;
    }
}
