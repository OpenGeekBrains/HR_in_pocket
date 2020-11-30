using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;

namespace HRInPocket.Interfaces.Services.Repository
{
    /// <summary>
    /// Сервис упраавления заданиями
    /// </summary>
    public interface ITargetTaskService : IDtoRepository<TargetTaskDTO>
    {
        /// <summary>
        /// Посомтреть все задания пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id);

        /// <summary>
        /// Выполнить задание по идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        Task<bool> ExecuteTargetTaskAsync(Guid id);

        /// <summary>
        /// Отменить задание
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        Task<bool> AbortTargetTaskAsync(Guid id);
    }
}
