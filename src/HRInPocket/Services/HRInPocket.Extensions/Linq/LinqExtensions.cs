using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool? condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition is null)
                return source;

            if ((bool)condition)
                return source.Where(predicate);

            return source;
        }

        /// <summary>
        /// Осуществляет выбор данных из коллекции в случае если выполняется условие 
        /// </summary>
        /// <typeparam name="TSource">Тип данных</typeparam>
        /// <param name="source">Коллекция</param>
        /// <param name="condition">Условие</param>
        /// <param name="predicate">Предикат выбора данных</param>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        /// <summary>
        /// Объединить несколько коллекций с одним типом. Возвращает объединенную перечисляемую коллекцию
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="collections">Коллекции</param>
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            IEnumerable<T> concatCollection = new List<T>();
            foreach (var collection in collections)
            {
                concatCollection = concatCollection.Concat(collection);
            }
            return concatCollection;
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
            if (source is IQueryable<TEntity>)
                return (IQueryable<TEntity>)source;

            return new EnumerableQuery<TEntity>(source);
        }
    }
}