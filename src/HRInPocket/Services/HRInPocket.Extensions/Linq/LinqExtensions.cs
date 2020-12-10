using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Extensions.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Осуществляет выбор данных из коллекции в случае если выполняется условие 
        /// </summary>
        /// <typeparam name="TSource">Тип данных</typeparam>
        /// <param name="source">Коллекция</param>
        /// <param name="condition">Условие, может быть Nullable типа</param>
        /// <param name="predicate">Предикат выбора данных</param>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool? condition, Expression<Func<TSource, bool>> predicate) =>
            condition switch
            {
                null => source,
                true => source.Where(predicate),
                _    => source
            };

        /// <summary>
        /// Осуществляет выбор данных из коллекции в случае если выполняется условие 
        /// </summary>
        /// <typeparam name="TSource">Тип данных</typeparam>
        /// <param name="source">Коллекция</param>
        /// <param name="condition">Условие</param>
        /// <param name="predicate">Предикат выбора данных</param>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate) => condition ? source.Where(predicate) : source;

        /// <summary>
        /// Объединить несколько коллекций с одним типом. Возвращает объединенную перечисляемую коллекцию
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="collections">Коллекции</param>
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            IEnumerable<T> concatCollection = new List<T>();
            return collections.Aggregate(concatCollection, (current, collection) => current.Concat(collection));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity">Тип данных</typeparam>
        /// <param name="source">Перечисляемая коллекция</param>
        public static async Task<IQueryable<TEntity>> AsQueryableAsync<TEntity>(this IEnumerable<TEntity> source) where TEntity : class
        {
            if (source is null)
                throw new ArgumentNullException();

            await Task.Delay(1);
            return source is IQueryable<TEntity> entities ? entities : new EnumerableQuery<TEntity>(source);
        }

        public static async Task<(IQueryable<T> Query, int TotalCount)> Page<T>(this IQueryable<T> query, int Page = 0, int PageSize = 0)
        {
            var total_count = await query.CountAsync().ConfigureAwait(false);
            if (Page > 0 && PageSize > 0)
                query = query.Skip(Page * PageSize);
            if (PageSize > 0)
                query = query.Take(PageSize);
            return (query, total_count);
        }
    }
}