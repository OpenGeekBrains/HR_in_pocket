﻿using System;
using System.Net.Http;
using System.Windows;

using HRInPocket.Clients.Vacancy;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Parsing.hh.ru.Interfaces;
using HRInPocket.Parsing.hh.ru.Service;
using HRInPocket.WPF.Services;
using HRInPocket.WPF.Services.Interfaces;
using HRInPocket.WPF.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddHttpClient<IDataRepository<Vacancy>, VacancyClient>();
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
