using HRInPocket.DAL.Models.Entities;
using HRInPocket.DAL.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRInPocket.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        #region Properties
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Speciality> Specialties { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<SystemManager> SystemManagers { get; set; } 
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<TargetTask> TargetTasks { get; set; }
        public DbSet<CoverLetter> CoverLetters { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);            // Никогда, ни зачто не удалять

            //model.Entity<Company>()
            //   .HasIndex(c => c.Inn)
            //   .IsUnique();

            //model.Entity<IdentityUserLogin<string>>()
            //   .HasKey(login => login.UserId);

            //model.Entity<Company>(company => 
            //{
            //    company.HasIndex(e => e.Inn).IsUnique();
            //});

            //model.Entity<Person>()
            //   .HasIndex(p => new { p.FirstName, p.LastName })
            //   .IsUnique(true);
        }
    }
}
