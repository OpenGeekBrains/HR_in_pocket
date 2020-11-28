using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Interfaces
{
    public interface IDataRepository<TEntity> where TEntity : BaseEntity
    {
        #region Get All

        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        Task<ICollection<TEntity>> GetAllAsync();

        #endregion

        #region Get Queryable

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        Task<IQueryable<TEntity>> GetQueryableAsync();

        #endregion

        #region Get By ID

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

        #endregion


        #region CRUD

        #region Create

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

        #endregion

        #region Edit

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

        #endregion

        #region Remove

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

        #endregion

        #region Create Range

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

        #endregion

        #region Remove Range

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

        #endregion 

        #endregion
    }
}