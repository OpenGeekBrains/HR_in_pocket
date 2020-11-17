﻿using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TestDbInitializer> _logger;

        public TestDbInitializer(ApplicationDbContext dbContext, ILogger<TestDbInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Initialize()
        {
            var db = _dbContext.Database;

            try
            {
                db.Migrate();

                _dbContext
                .InitTable(TestData.Addresses)
                .InitTable(TestData.Specialties)
                .InitTable(TestData.ActivityCategories)
                .InitTable(TestData.TargetTasks)

                .InitTable(TestData.Tarifs)
                .InitTable(TestData.Price)



                ////.InitTable(TestData.Vacancies)           // Not Filled
                ////.InitTable(TestData.CoverLetters)        // Not Filled
                ////.InitTable(TestData.Companies)           // Not Filled
                ////.InitTable(TestData.Profiles)            // Not Filled

                ////.InitTable(TestData.Users)                // Rework for Identity integration
                ////.InitTable(TestData.Applicants)           // Rework for Identity integration
                ////.InitTable(TestData.SystemManagers)       // Rework for Identity integration

                ////.InitTable(TestData.Resumes)              // Behave on Identity Entities
                ;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Ошибка инициализации БД");
                throw;
            }
        }
    }
}
