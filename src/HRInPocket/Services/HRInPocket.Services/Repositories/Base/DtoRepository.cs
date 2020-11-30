using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO.Base;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository.Base;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories.Base
{
    public abstract class DtoRepository<TEntity,TDto> : IDtoRepository<TDto> 
        where TDto : BaseDTO 
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        protected readonly IDataRepository<TEntity> _DataProvider;

        /// <summary>
        /// Конвертер данных
        /// </summary>
        protected readonly IMapper _Mapper;

        protected DtoRepository(IDataRepository<TEntity> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        #region IRepository implementation

        /// <inheritdoc/>
        public virtual async Task<PageDTOs<TDto>> GetAllAsync(Filter filter = null)
        {
            var query = await _DataProvider.GetQueryableAsync();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/




                query = query
                    .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                    .Take(filter.Pages.PageSize);
            }

            var count = await query.CountAsync();

            return new PageDTOs<TDto>
            {
                Entities = query.AsEnumerable().Select(_Mapper.Map<TDto>),
                TotalCount = count
            };
        }

        /// <inheritdoc/>
        public virtual async Task<TDto> GetByIdAsync(Guid id) => _Mapper.Map<TDto>(await _DataProvider.GetByIdAsync(id));

        /// <inheritdoc/>
        public virtual async Task<Guid> CreateAsync(TDto company) => await _DataProvider.CreateAsync(_Mapper.Map<TEntity>(company));

        /// <inheritdoc/>
        public virtual async Task<bool> EditAsync(TDto company) => await _DataProvider.EditAsync(_Mapper.Map<TEntity>(company));

        /// <inheritdoc/>
        public virtual async Task<bool> RemoveAsync(Guid id) => await _DataProvider.RemoveAsync(id);

        #endregion
    }
}