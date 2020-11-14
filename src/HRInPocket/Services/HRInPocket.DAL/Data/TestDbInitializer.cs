using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer
    {
        private readonly ApplicationDbContext _DBContext;
        private readonly ILogger<TestDbInitializer> _Logger;

        public TestDbInitializer(ApplicationDbContext DBContext, ILogger<TestDbInitializer> logger)
        {
            this._DBContext = DBContext;
            this._Logger = logger;
        }

        public void Initialize()
        {
            var db = _DBContext.Database;

            try
            {
                db.Migrate();

                //_DBContext
                //.InitTable(TestData.Addresses)
                //.InitTable(TestData.Specialties)
                //.InitTable(TestData.ActivityCategories)
                //.InitTable(TestData.Applicants)
                //.InitTable(TestData.Resumes)
                //.InitTable(TestData.Companies)
                //.InitTable(TestData.Vacancies)
                //.InitTable(TestData.SystemManagers)
                //.InitTable(TestData.Tarifs)
                //.InitTable(TestData.TargetTasks)
                //.InitTable(TestData.CoverLetters)
                //.InitTable(TestData.Profiles)
                //.InitTable(TestData.Users)
                //;
                
            }
            catch (Exception e)
            {
                _Logger.LogError(e.Message, "Ошибка инициализации БД");
                throw;
            }
        }
    }
}
