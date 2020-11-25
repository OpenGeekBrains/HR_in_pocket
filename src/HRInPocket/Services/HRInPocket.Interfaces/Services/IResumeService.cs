using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
using HRInPocket.Domain.Models.Resume;

namespace HRInPocket.Interfaces.Services
{
    /// <summary>
    /// Сервис управления резюме
    /// </summary>
    public interface IResumeService
    {
        /// <summary>
        /// Посомтреть весь список резюме
        /// </summary>
        Task<PageResumeDTO> GetResumesAsync(ResumeFilter filter);

        /// <summary>
        /// Посмотреть список резюме пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id);

        /// <summary>
        /// Посомтреть информацию о резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        Task<ResumeDTO> GetResumeByIdAsync(Guid id);

        /// <summary>
        /// Создать резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        Task<Guid> CreateResumeAsync(ResumeDTO resume);

        /// <summary>
        /// Редактировать информацию в резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        Task<bool> EditResumeAsync(ResumeDTO resume);

        /// <summary>
        /// Удалить резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        Task<bool> RemoveResumeAsync(Guid id);

        /// <summary>
        /// Загрузить файл резюме
        /// </summary>
        /// <param name="resumeFile">Модель файла резюме</param>
        Task<bool> UploadResumeFileAsync(ResumeFile resumeFile);


        // Методы поиска резюме

        Task SearchResumesAsync();
    }
}