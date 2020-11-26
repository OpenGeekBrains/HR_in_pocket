using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Interfaces
{
    public interface IDataRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        Task<IQueryable<TEntity>> GetQueryableAsync();

        /// <summary>
        /// Запросить объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        TEntity GetById(Guid id);

        /// <summary>
        /// Запросить объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item">Объект</param>
        Guid Create(TEntity item);

        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item">Объект</param>
        Task<Guid> CreateAsync(TEntity item);

        /// <summary>
        /// Редактировать объект в базе данных 
        /// </summary>
        /// <param name="item">Объект</param>
        bool Edit(TEntity item);

        /// <summary>
        /// Редактировать объект в базе данных 
        /// </summary>
        /// <param name="item">Объект</param>
        Task<bool> EditAsync(TEntity item);

        /// <summary>
        /// Удалить объект из базы данных
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        bool Remove(Guid id);

        /// <summary>
        /// Удалить объект из базы данных
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        Task<bool> RemoveAsync(Guid id);

        /// <summary>
        /// Создать диапазон объектов в базе данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        void CreateRange(ICollection<TEntity> items);

        /// <summary>
        /// Создать диапазон объектов в базе данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        Task CreateRangeAsync(ICollection<TEntity> items);

        /// <summary>
        /// Удалить диапазон объектов из базы данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        bool RemoveRange(ICollection<TEntity> items);

        /// <summary>
        /// Удалить диапазон объектов из базы данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        Task<bool> RemoveRangeAsync(ICollection<TEntity> items);
    }
}