using System;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Repository.Base;

namespace HRInPocket.Interfaces.Repository
{
    public interface IVacancyService : IDtoRepository<Vacancy,VacancyDTO>
    {
        /// <summary>
        /// Посмотреть вакансии компании по ее идентификатору
        /// </summary>
        /// <param name="id">Иденификатор компании</param>
        Task<PageDTOs<VacancyDTO>> GetCompanyVacanciesAsync(Guid id);

        // Методы поиска вакансий (через API, из тех которые были сформированы по результатам парсинга сайтов)

        /// <summary>
        /// Поиск вакансий
        /// </summary>
        Task SearchVacanciesAsync();
    }
}
