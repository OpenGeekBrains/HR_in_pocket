using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HRInPocket.IdentityServer.Data
{
    public static class UsersDbInitializer
    {
        static readonly ILogger __Logger = Log.Logger;
        static bool __Flag = false;
        public static void Init(IServiceProvider ScopServiceProvider)
        {
            var db = ScopServiceProvider.GetService<UsersDbContext>().Database;
            var user_manager = ScopServiceProvider.GetService<UserManager<ApplicationUser>>();

            __Logger.Debug("Check migrations...");
            if (db.GetPendingMigrations().Any())
            {
                if (!__Flag) __Flag = true;
                __Logger.Debug("Run database migration");
                db.Migrate();
                __Logger.Debug("Migration completed");
            }
            else __Logger.Debug("Migration is not required");

            __Logger.Debug("Check users...");
            InitializeIdentityAsync(user_manager).Wait();

            var str = __Flag ? "completed" : "not required";
            __Logger.Debug($"User database update is {str}");
        }

        private static async Task InitializeIdentityAsync(UserManager<ApplicationUser> UserManager)
        {
            var str = "not required";
            var str1 = "not required";

            foreach (var DefaultUser in DefaultConfig.GetUsers())
            {
                var role_name = DefaultUser.Claims.First(c => c.Type.Equals("role")).Value;

                __Logger.Debug($"Cheking of \"{role_name}\" role users...");
                if (await UserManager.FindByNameAsync(DefaultUser.Username) is null)
                {
                    if (!__Flag) __Flag = true;
                    __Logger.Debug($"No user with the \"{role_name}\" role was found");

                    var user = new ApplicationUser { UserName = DefaultUser.Username };

                    __Logger.Debug($"Registering a user with the \"{role_name}\" role...");
                    var result = await UserManager.CreateAsync(user, DefaultUser.Password);

                    var str2 = result.Succeeded ? "was successful" : "has failed";
                    __Logger.Debug($"Registration of the user with the role \"{role_name}\" {str2}");

                    if (!result.Succeeded) continue;

                    __Logger.Debug("Registration of user claims...");
                    foreach (var claim in DefaultUser.Claims) await UserManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                    __Logger.Debug("Registration of user claims completed");

                    str = "completed";
                }
                else __Logger.Debug($"Registering a user with the \"{role_name}\" role is {str}");
            }
            __Logger.Debug($"Registration of users is {str1}");
        }
    }
}
