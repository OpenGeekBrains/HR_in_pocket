using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HRInPocket.IdentityServer.InMemoryConfig;
using HRInPocket.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.IdentityServer.Data
{
    public static class UsersDbInitializer
    {
        public static void Init(IServiceProvider ScopServiceProvider)
        {
            var db = ScopServiceProvider.GetService<UsersDbContext>().Database;
            var user_manager = ScopServiceProvider.GetService<UserManager<ApplicationUser>>();

            if (db.GetPendingMigrations().Any()) db.Migrate();

            InitializeIdentityAsync(user_manager).Wait();
        }

        private static async Task InitializeIdentityAsync(UserManager<ApplicationUser> UserManager)
        {
            foreach (var DefaultUser in DefaultConfig.GetUsers())
            {
                if (!(await UserManager.FindByNameAsync(DefaultUser.Username) is null)) continue;
                var user = new ApplicationUser {UserName = DefaultUser.Username};

                var result = UserManager.CreateAsync(user, DefaultUser.Password).GetAwaiter().GetResult();
                if (!result.Succeeded) continue;
                foreach (var claim in DefaultUser.Claims) UserManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value)).GetAwaiter().GetResult();
            }
        }
    }
}
