using System;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Services
{
    public interface IVacancyService
    {
        /// <summary>
        /// Посмотреть все вакансии
        /// </summary>
        Task<PageVacancyDTO> GetVacanciesAsync(VacancyFilter filter);
        
        /// <summary>
        /// Посмотреть вакансии компании по ее идентификатору
        /// </summary>
        /// <param name="id">Иденификатор компании</param>
        Task<PageVacancyDTO> GetCompanyVacanciesAsync(Guid id);

        /// <summary>
        /// Создать вакансию
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        Task<Guid> CreateVacancyAsync(VacancyDTO vacancy);

        /// <summary>
        /// Редактировать информацию в вакансии
        /// </summary>
        /// <param name="vacancy">Модель представления вакансии</param>
        Task<bool> EditVacancyAsync(VacancyDTO vacancy);

        /// <summary>
        /// Удалить вакансию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вакансии</param>
        Task<bool> RemoveVacancyAsync(Guid id);


        // Методы поиска вакансий (через API, из тех которые были сформированы по результатам парсинга сайтов)

        /// <summary>
        /// Поиск вакансий
        /// </summary>
        Task SearchVacanciesAsync();
    }
}
