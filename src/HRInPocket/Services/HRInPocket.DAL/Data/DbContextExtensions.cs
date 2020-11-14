using System;
using System.Collections.Generic;
using System.Linq;
using HRInPocket.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace HRInPocket.DAL.Data
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Инициализирует таблицу типа данными из перечисления, если они есть и таблица не была до этого инициализирована
        /// </summary>
        /// <typeparam name="TEntity">Тип наследованный от 'BaseEntity'</typeparam>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="data">Данные для инициализации таблицы</param>
        /// <returns>Контекст базы данных с которым проводилась работа</returns>
        public static DbContext InitTable<TEntity>(this DbContext context, IEnumerable<TEntity> data) where TEntity : class
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            var entities = data as TEntity[] ?? data.ToArray();
            if(entities.Length == 0) throw new ArgumentException("There no data to import", nameof(data));

            var table = context.Set<TEntity>();
            if (table.Any()) return context;

            using var transaction = context.Database.BeginTransaction();
            table.AddRange(entities);
            context.SaveChanges();
            transaction.Commit();

            return context;
        }
    }
}