using System;
using System.Net.Http;
using System.Windows;
using AutoMapper;
using HRInPocket.Domain;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Repository.Base;
using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Service;
using HRInPocket.WPF.Services;
using HRInPocket.WPF.Services.Interfaces;
using HRInPocket.WPF.ViewModels;
using  HRInPocket.Clients.Vacancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using static HRInPocket.WPF.Data.TestData;

namespace HRInPocket.WPF
{
    public partial class App : Application
    {
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<ISaveDataToJSON, SaveDataToJSON>();
            services.AddSingleton<IParsehhService, ParsehhService>();
            services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["WebAPI"]) });
            services.AddScoped<IDataRepository<Vacancy>, VacancyClient>();
            services.AddAutoMapperWithProfiles(
                typeof(MappingProfile)
            );
        }

        private static IHost _Host;
        public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }
    }
}
