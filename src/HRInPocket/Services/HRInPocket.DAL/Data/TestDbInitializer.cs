using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly ILogger<TestDbInitializer> _Logger;

        public TestDbInitializer(ApplicationDbContext DbContext, ILogger<TestDbInitializer> Logger)
        {
            _DbContext = DbContext;
            _Logger = Logger;
        }

        public void Initialize()
        {
            var db = _DbContext.Database;

            try
            {
                db.Migrate();

                InitializeAddress();
                InitializeSpecialities();
                InitializeActivityCategory();
                InitializeApplicant();
                InitializeResume();
                InitializeCompany();
                InitializeVacancies();
                InitializeCompanyManager();
                InitializeEmployer(); 
                InitializeSystemManager();

            }
            catch (Exception e)
            {
                _Logger.LogError(e, "Ошибка инициализации БД");
                throw;
            }
        }

        private void InitializeAddress()
        {
            //var test = new StreamReader("testfile.txt").DisposeAfter(file => file.ReadToEnd());

            if (_DbContext.Addresses.Any()) return;

            _DbContext.Database.BeginTransaction().DisposeAfter(
                transaction =>
                {
                    _DbContext.Addresses.AddRange(TestData.Addresses);

                    _DbContext.SaveChanges();

                    transaction.Commit();
                });

            //var db = _DbContext.Database;
            //using (db.BeginTransaction())
            //{
            //    _DbContext.Addresses.AddRange(TestData.Addresses);

            //    _DbContext.SaveChanges();

            //    db.CommitTransaction();
            //}
        }

        private void InitializeSpecialities()
        {
            if (_DbContext.Specialties.Any()) return;
            
            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Specialties.AddRange(TestData.Specialties);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeActivityCategory()
        {
            if (_DbContext.ActivityCategories.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.ActivityCategories.AddRange(TestData.ActivityCategories);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeVacancies()
        {
            if (_DbContext.Vacancies.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Vacancies.AddRange(TestData.Vacancies);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeResume()
        {
            if (_DbContext.Resumes.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Resumes.AddRange(TestData.Resumes);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeCompany()
        {
            if (_DbContext.Companies.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Companies.AddRange(TestData.Companies);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeApplicant()
        {
            if (_DbContext.Applicants.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Applicants.AddRange(TestData.Applicants);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeCompanyManager()
        {
            if (_DbContext.CompanyManagers.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.CompanyManagers.AddRange(TestData.CompanyManagers);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeEmployer()
        {
            if (_DbContext.Employers.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.Employers.AddRange(TestData.Employers);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }

        private void InitializeSystemManager()
        {
            if (_DbContext.SystemManagers.Any()) return;

            var db = _DbContext.Database;
            using (db.BeginTransaction())
            {
                _DbContext.SystemManagers.AddRange(TestData.SystemManagers);

                _DbContext.SaveChanges();

                db.CommitTransaction();
            }
        }
    }
}
