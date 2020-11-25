using System;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Services
{
    public interface ICompanyService
    {
        /// <summary>
        /// Посмотреть информацию о всех компаниях
        /// </summary>
        Task<PageCompanyDTO> GetCompanies(CompanyFilter filter = null);

        /// <summary>
        /// Посмотреть информацию о компании по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        Task<CompanyDTO> GetCompanyById(Guid id);

        /// <summary>
        /// Создать компанию
        /// </summary>
        /// <param name="company">Модель компании</param>
        Task<Guid> CreateCompanyAsync(CompanyDTO company);

        /// <summary>
        /// Редактирвание информации о компании
        /// </summary>
        /// <param name="company">Модель компании</param>
        Task<bool> EditCompanyAsync(CompanyDTO company);

        /// <summary>
        /// Удаление компании по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор компании</param>
        Task<bool> RemoveCompanyAsync(Guid id);
    }
}