using System;
using System.Windows;

using HRInPocket.WPF.Services;
using HRInPocket.WPF.Services.Interfaces;
using HRInPocket.WPF.ViewModels;

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
