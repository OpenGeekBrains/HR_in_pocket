using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Подключение к тестовому IdentityServer
                //builder.Configuration.Bind("TestDemo", options.ProviderOptions);
                // ---------------------------------------------------------------
                
                // Подключение к HRInPocket.IdentityServer
                builder.Configuration.Bind("HRInPocket_IdentityServer", options.ProviderOptions);
                //----------------------------------------------------------------------------------
            });

            await builder.Build().RunAsync();
        }
    }
}
