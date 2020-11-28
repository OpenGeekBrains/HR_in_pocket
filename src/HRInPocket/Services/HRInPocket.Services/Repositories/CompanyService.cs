using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services.Repository;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories
{
    public class CompanyService : ICompanyService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IDataRepository<Company> _DataProvider;
        private readonly IMapper _Mapper;

        public CompanyService(IDataRepository<Company> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        #region IRepository implementation

        /// <summary>
        /// Посмотреть информацию о всех компаниях
        /// </summary>
        public async Task<PageDTOs<CompanyDTO>> GetAllAsync(Filter filter = null)
        {
            var query = _DataProvider.GetQueryable();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/
            }

            var count = await query.CountAsync();

            query = query
                .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                .Take(filter.Pages.PageSize);

            return new PageDTOs<CompanyDTO>
            {
                Entities = query.Select(q => _Mapper.Map<CompanyDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть информацию о компании по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        public async Task<CompanyDTO> GetByIdAsync(Guid id) =>
            _Mapper.Map<CompanyDTO>((await _DataProvider.GetByIdAsync(id)));

        /// <summary>
        /// Создать компанию
        /// </summary>
        /// <param name="company">Модель компании</param>
        public async Task<Guid> CreateAsync(CompanyDTO company) =>
            await _DataProvider.CreateAsync(_Mapper.Map<Company>(company));

        /// <summary>
        /// Редактирвание информации о компании
        /// </summary>
        /// <param name="company">Модель компании</param>
        public async Task<bool> EditAsync(CompanyDTO company) =>
            await _DataProvider.EditAsync(_Mapper.Map<Company>(company));

        /// <summary>
        /// Удаление компании по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        public async Task<bool> RemoveAsync(Guid id) =>
            await _DataProvider.RemoveAsync(id); 

        #endregion
    }
}
