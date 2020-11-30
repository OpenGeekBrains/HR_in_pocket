using System;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;

namespace HRInPocket.Interfaces.Services.Repository
{
    public interface IVacancyService : IDtoRepository<VacancyDTO>
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
