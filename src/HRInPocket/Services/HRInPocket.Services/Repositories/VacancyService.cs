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
    public class VacancyService : IVacancyService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IDataRepository<Vacancy> _DataProvider;
        private readonly IMapper _Mapper;

        public VacancyService(IDataRepository<Vacancy> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        #region IRepository implementation

        /// <summary>
        /// Посмотреть все вакансии
        /// </summary>
        public async Task<PageDTOs<VacancyDTO>> GetAllAsync(Filter filter)
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

            

            return new PageDTOs<VacancyDTO>
            {
                Entities = query.AsEnumerable().Select(_Mapper.Map<VacancyDTO>),
                TotalCount = count
            };
        }

        public Task<VacancyDTO> GetByIdAsync(Guid id) => throw new NotImplementedException();

        /// <summary>
        /// Создать вакансию
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        public async Task<Guid> CreateAsync(VacancyDTO vacancy) => await _DataProvider.CreateAsync(_Mapper.Map<Vacancy>(vacancy));

        /// <summary>
        /// Редактировать информацию в вакансии
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        public async Task<bool> EditAsync(VacancyDTO vacancy) => await _DataProvider.EditAsync(_Mapper.Map<Vacancy>(vacancy));

        /// <summary>
        /// Удалить вакансию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вакансии</param>
        public async Task<bool> RemoveAsync(Guid id) => await _DataProvider.RemoveAsync(id); 

        #endregion




        /// <inheritdoc/>
        public async Task<PageDTOs<VacancyDTO>> GetCompanyVacanciesAsync(Guid id)
        {
            var query = (await _DataProvider.GetQueryableAsync())
                .Where(v => v.Company.Id == id);

            var count = await query.CountAsync();

            return new PageDTOs<VacancyDTO>
            {
                Entities = query.AsEnumerable().Select(_Mapper.Map<VacancyDTO>),
                TotalCount = count,
            };
        }

        // Методы поиска вакансий (через API, из тех которые были сформированы по результатам парсинга сайтов)

        /// <inheritdoc/>
        public Task SearchVacanciesAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}