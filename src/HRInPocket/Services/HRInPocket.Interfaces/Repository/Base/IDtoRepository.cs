using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO.Base;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Repository.Base
{
    public interface IDtoRepository<TEntity,TDto> where TDto: BaseDTO where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить страницу записей по <paramref name="filter"/>, если он указан
        /// </summary>
        /// <param name="filter">Фильтр</param>
        Task<PageDTOs<TDto>> GetAllAsync(Filter filter = null);

        /// <summary>
        /// Посмотреть информацию о записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<TDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="model">Запись</param>
        Task<Guid> CreateAsync(TDto model);

        /// <summary>
        /// Редактирвание записи
        /// </summary>
        /// <param name="model">Запись</param>
        Task<bool> EditAsync(TDto model);

        /// <summary>
        /// Удаление записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        Task<bool> RemoveAsync(Guid id);

        /// <summary>
        /// создать диапозон объектов
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task CreateRangeAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// удалить диапозон объектов
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> items);
    }
}