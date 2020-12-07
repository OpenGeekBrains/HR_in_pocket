using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Interfaces.EF
{
    public interface IDbInitializer
    {
        void Initialize();

        readonly ref struct Initializer
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