using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using HRInPocket.IdentityServer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                ServerDbInitializer.Init(scope.ServiceProvider);
                UsersDbInitializer.Init(scope.ServiceProvider);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(WebBuilder =>
                {
                    WebBuilder.UseStartup<Startup>();
                });
    }
}
