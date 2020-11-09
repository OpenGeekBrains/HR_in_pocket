﻿using System;
using System.Collections.Generic;
using System.Text;
using HRInPocket.DAL.Models.Entities;
using HRInPocket.DAL.Models.Users;
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
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<CompanyManager> CompanyManagers { get; set; }
        public DbSet<Employer> Employers { get; set; } 
        public DbSet<SystemManager> SystemManagers { get; set; } 
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Company>()
               .HasIndex(c => c.Inn)
               .IsUnique();

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