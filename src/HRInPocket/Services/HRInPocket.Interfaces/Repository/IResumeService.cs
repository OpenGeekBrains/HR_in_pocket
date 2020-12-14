using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Models.Resume;
using HRInPocket.Interfaces.Repository.Base;

namespace HRInPocket.Interfaces.Repository
{
    /// <summary>
    /// Сервис управления резюме
    /// </summary>
    public interface IResumeService : IDtoRepository<Resume, ResumeDTO>
    {
        /// <summary>
        /// Посмотреть список резюме пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id);

        /// <summary>
        /// Загрузить файл резюме
        /// </summary>
        /// <param name="resumeFile">Модель файла резюме</param>
        Task<bool> UploadResumeFileAsync(ResumeFile resumeFile);


        // Методы поиска резюме

        Task SearchResumesAsync();
    }
}