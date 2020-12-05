﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Services
{
    /// <summary>
    /// Сервис упраавления заданиями
    /// </summary>
    public interface ITargetTaskService
    {
        /// <summary>
        /// Посомтреть все задания
        /// </summary>
        Task<PageTargetTaskDTO> GetAllTargetTasksAsync(TargetTaskFilter filter= null);

        /// <summary>
        /// Посомтреть все задания пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id);

        /// <summary>
        /// Посмотреть информацию задания по его идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        Task<TargetTaskDTO> GetTargetTaskByIdAsync(Guid id);

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        Task<Guid> CreateTargetTaskAsync(TargetTaskDTO task);

        /// <summary>
        /// Редактировать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        Task<bool> EditTargetTaskAsync(TargetTaskDTO task);

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

        /// <summary>
        /// Удалить задание
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        Task<bool> RemoveTargetTaskAsync(Guid id);
    }
}
