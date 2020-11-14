using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TestDbInitializer> logger;

        public TestDbInitializer(ApplicationDbContext dbContext, ILogger<TestDbInitializer> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void Initialize()
        {
            var db = dbContext.Database;

            try
            {
                db.Migrate();

                dbContext
                .InitTable(TestData.Addresses)
                .InitTable(TestData.Specialties)
                .InitTable(TestData.ActivityCategories)
                //.InitTable(TestData.Applicants)
                .InitTable(TestData.Resumes)
                .InitTable(TestData.Companies)
                .InitTable(TestData.Vacancies)
                //.InitTable(TestData.CompanyManagers)
                .InitTable(TestData.Employers)
                .InitTable(TestData.SystemManagers)
                .InitTable(TestData.Tarifs)
                .InitTable(TestData.TargetTasks);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, "Ошибка инициализации БД");
                throw;
            }
        }
    }
}
