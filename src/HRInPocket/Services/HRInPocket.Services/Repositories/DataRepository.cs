using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.DAL.Data;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Extensions.Linq;
using HRInPocket.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories
{
    public class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Контекст базы данных </summary>
        private readonly ApplicationDbContext _DbContext;

        public DataRepository(ApplicationDbContext db) => _DbContext = db;

        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        public ICollection<TEntity> GetAll() =>
            _DbContext.Set<TEntity>().ToArray();

        /// <summary>
        /// Запросить все данные из таблицы
        /// </summary>
        public async Task<ICollection<TEntity>> GetAllAsync() =>
            await _DbContext.Set<TEntity>().ToArrayAsync();

        /// <summary>
        /// Запросить объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        public TEntity GetById(Guid id) =>
            _DbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Запросить объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        public async Task<TEntity> GetByIdAsync(Guid id) =>
            await _DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        public IQueryable<TEntity> GetQueryable() => _DbContext.Set<TEntity>();

        /// <summary>
        /// Запросить все данные из таблицы. Возвращает коллекцию объектов в виде IQueryable<TEntity>
        /// </summary>
        public async Task<IQueryable<TEntity>> GetQueryableAsync() => 
            await _DbContext.Set<TEntity>().AsQueryableAsync();

        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item">Объект</param>
        public Guid Create(TEntity item)
        {
            if (item is null) 
                throw new ArgumentNullException(nameof(item));

            var table = _DbContext.Set<TEntity>();
            if (table.Any(r => r.Id == item.Id))
                return item.Id;

            table.Add(item);
            _DbContext.SaveChanges();
            return item.Id;
        }

        /// <summary>
        /// Создать объект в базе данных
        /// </summary>
        /// <param name="item">Объект</param>
        public async Task<Guid> CreateAsync(TEntity item)
        {
            if (item is null) 
                throw new ArgumentNullException(nameof(item));

            var table = _DbContext.Set<TEntity>();
            if (await table.AnyAsync(r => r.Id == item.Id))
                return item.Id;

            await table.AddAsync(item);
            await _DbContext.SaveChangesAsync();
            return item.Id;
        }

        /// <summary>
        /// Создать диапазон объектов в базе данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        public void CreateRange(ICollection<TEntity> items)
        {
            if (items is null) 
                throw new ArgumentNullException(nameof(items));

            var table = _DbContext.Set<TEntity>();
            table.AddRange(items);
            _DbContext.SaveChanges();
        }

        /// <summary>
        /// Создать диапазон объектов в базе данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        public async Task CreateRangeAsync(ICollection<TEntity> items)
        {
            if (items is null) 
                throw new ArgumentNullException(nameof(items));

            var table = _DbContext.Set<TEntity>();
            await table.AddRangeAsync(items);
            await _DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Редактировать объект в базе данных 
        /// </summary>
        /// <param name="item">Объект</param>
        public bool Edit(TEntity item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _DbContext.Attach(item);
            _DbContext.Update(item);
            _DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Редактировать объект в базе данных 
        /// </summary>
        /// <param name="item">Объект</param>
        public async Task<bool> EditAsync(TEntity item)
        {
            if (item is null) 
                throw new ArgumentNullException(nameof(item));

            _DbContext.Attach(item);
            _DbContext.Update(item);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удалить объект из базы данных
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        public bool Remove(Guid id)
        {
            var table = _DbContext.Set<TEntity>();

            var item = table.FirstOrDefault(s => s.Id == id);
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            table.Remove(item);
            _DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Удалить объект из базы данных
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var table = _DbContext.Set<TEntity>();

            var item = await table.FirstOrDefaultAsync(s => s.Id == id);
            if (item is null) 
                throw new ArgumentNullException(nameof(item));

            table.Remove(item);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удалить диапазон объектов из базы данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        public bool RemoveRange(ICollection<TEntity> items)
        {
            var table = _DbContext.Set<TEntity>();
            table.RemoveRange(items);
            _DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Удалить диапазон объектов из базы данных
        /// </summary>
        /// <param name="items">Диапазон объектов</param>
        public async Task<bool> RemoveRangeAsync(ICollection<TEntity> items)
        {
            var table = _DbContext.Set<TEntity>();
            table.RemoveRange(items);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
