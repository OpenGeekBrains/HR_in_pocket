using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Services
{
    public class VacancyService : IVacancyService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository<Vacancy> _DataProvider;
        private readonly IMapper _Mapper;

        public VacancyService(IDataRepository<Vacancy> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        /// <summary>
        /// Посмотреть все вакансии
        /// </summary>
        public async Task<PageVacancyDTO> GetVacanciesAsync(VacancyFilter filter = null)
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

            return new PageVacancyDTO
            {
                Vacancies = query.Select(q => _Mapper.Map<VacancyDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть вакансии компании по ее идентификатору
        /// </summary>
        /// <param name="id">Иденификатор компании</param>
        public async Task<PageVacancyDTO> GetCompanyVacanciesAsync(Guid id)
        {
            var query = _DataProvider.GetQueryable()
                .Where(v => v.Company.Id == id);

            var count = await query.CountAsync();

            return new PageVacancyDTO
            {
                Vacancies = query.Select(q => _Mapper.Map<VacancyDTO>(q)),
                TotalCount = count,
            };
        }

        /// <summary>
        /// Создать вакансию
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        public async Task<Guid> CreateVacancyAsync(VacancyDTO vacancy) => await _DataProvider.CreateAsync(_Mapper.Map<Vacancy>(vacancy));

        /// <summary>
        /// Редактировать информацию в вакансии
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        public async Task<bool> EditVacancyAsync(VacancyDTO vacancy) => await _DataProvider.EditAsync(_Mapper.Map<Vacancy>(vacancy));

        /// <summary>
        /// Удалить вакансию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вакансии</param>
        public async Task<bool> RemoveVacancyAsync(Guid id) => await _DataProvider.RemoveAsync(id);

        // Методы поиска вакансий (через API, из тех которые были сформированы по результатам парсинга сайтов)

        /// <summary>
        /// Поиск вакансий
        /// </summary>
        public Task SearchVacanciesAsync() => throw new NotImplementedException();
    }
}