using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.DAL.Data
{
    public class TestDbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TestDbInitializer> _logger;

        public TestDbInitializer(ApplicationDbContext dbContext, ILogger<TestDbInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            //logger.LogError("Ошибка в конструкторе БД");
        }

        public void Initialize()
        {
            var db = _dbContext.Database;

            try
            {
                db.Migrate();

                IDbInitializer.Initializer.StartInit(_dbContext, _logger)
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

    public interface IDbInitializer
    {
        void Initialize();
        
        readonly struct Initializer
        {
            public readonly DbContext Context;
            private readonly ILogger<IDbInitializer> _logger;

            private Initializer(DbContext context, ILogger<IDbInitializer> logger)
            {
                Context = context;
                _logger = logger;
            }

            public static Initializer StartInit(DbContext context, ILogger<IDbInitializer> logger) => new Initializer(context, logger);

            /// <summary>
            /// Инициализирует таблицу типа данными из перечисления, если они есть и таблица не была до этого инициализирована
            /// </summary>
            /// <typeparam name="TEntity">Тип наследованный от 'BaseEntity'</typeparam>
            /// <param name="data">Данные для инициализации таблицы</param>
            /// <returns>Контекст базы данных с которым проводилась работа</returns>
            public Initializer InitTable<TEntity>(IEnumerable<TEntity> data) where TEntity : class
            {
                _ = data ?? throw new ArgumentNullException(nameof(data));
                var entities = data as TEntity[] ?? data.ToArray();
                if (entities.Length == 0) throw new ArgumentException("There no data to import", nameof(data));


                var table = Context.Set<TEntity>();
                if (table.Any()) return this;

                using var transaction = Context.Database.BeginTransaction();
                table.AddRange(entities);
                Context.SaveChanges();
                transaction.Commit();

                return this;
            }
        }
    }
}
