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
        /// Контекст базы данных
        /// </summary>
        private readonly ApplicationDbContext _DbContext;

        public DataRepository(ApplicationDbContext db) => _DbContext = db;

        #region Get All

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll() => _DbContext.Set<TEntity>().ToArray();

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _DbContext.Set<TEntity>().ToArrayAsync();

        #endregion

        #region Get By ID

        /// <inheritdoc/>
        public TEntity GetById(Guid id) => _DbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(Guid id) => await _DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

        #endregion

        #region Get Queryable

        /// <inheritdoc/>
        public IQueryable<TEntity> GetQueryable() => _DbContext.Set<TEntity>();

        /// <inheritdoc/>
        public async Task<IQueryable<TEntity>> GetQueryableAsync() =>
            await _DbContext.Set<TEntity>().AsQueryableAsync();

        #endregion



        #region CRUD

        #region Create

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        #endregion

        #region Edit

        /// <inheritdoc/>
        public bool Edit(TEntity item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _DbContext.Attach(item);
            _DbContext.Update(item);
            _DbContext.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> EditAsync(TEntity item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _DbContext.Attach(item);
            _DbContext.Update(item);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Remove

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        #endregion


        #region Create Range

        /// <inheritdoc/>
        public void CreateRange(IEnumerable<TEntity> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var table = _DbContext.Set<TEntity>();
            table.AddRange(items);
            _DbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task CreateRangeAsync(IEnumerable<TEntity> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var table = _DbContext.Set<TEntity>();
            await table.AddRangeAsync(items);
            await _DbContext.SaveChangesAsync();
        }

        #endregion

        #region Remove Range

        /// <inheritdoc/>
        public bool RemoveRange(IEnumerable<TEntity> items)
        {
            var table = _DbContext.Set<TEntity>();
            table.RemoveRange(items);
            _DbContext.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> items)
        {
            var table = _DbContext.Set<TEntity>();
            table.RemoveRange(items);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        #endregion 

        #endregion
    }
}
