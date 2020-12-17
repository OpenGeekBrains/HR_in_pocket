using System;
using HRInPocket.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRInPocket.IdentityServer.Data
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
    }
}
