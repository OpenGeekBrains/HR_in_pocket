using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Entities.Profiles;
using Microsoft.EntityFrameworkCore;

namespace HRInPocket.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Properties
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Speciality> Specialties { get; set; }
        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApplicantProfile> ApplicantProfiles { get; set; }
        public DbSet<ManagerProfile> ManagerProfiles { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<TargetTask> TargetTasks { get; set; }
        public DbSet<CoverLetter> CoverLetters { get; set; }
        public DbSet<ApplicantProfile> Profiles { get; set; }
        public DbSet<PriceItem> Price { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);            // Необходимо для работы системы Identity
            // Все модификации указывать ниже

        }
    }
}
