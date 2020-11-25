using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.DAL.Models.Entities;
using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Services
{
    public class TargetTaskService : ITargetTaskService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository _DataProvider;
        private readonly IMapper<TargetTask, TargetTaskDTO> _Mapper;

        public TargetTaskService(IDataRepository dataProvider, IMapper<TargetTask, TargetTaskDTO> mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        /// <summary>
        /// Посомтреть все задания
        /// </summary>
        public async Task<PageTargetTaskDTO> GetAllTargetTasksAsync(TargetTaskFilter filter)
        {
            var query = _DataProvider.GetQueryable<TargetTask>();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/
            }

            var count = await query.CountAsync();

            query = query
                .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                .Take(filter.Pages.PageSize);

            return new PageTargetTaskDTO
            {
                TargetTasks = query.Select(q => _Mapper.Map(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть информацию задания по его идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<TargetTaskDTO> GetTargetTaskByIdAsync(long id) => _Mapper.Map((await _DataProvider.GetByIdAsync<TargetTask>(id)));

        /// <summary>
        /// Посомтреть все задания пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => 
            (await _DataProvider.GetQueryableAsync<TargetTask>())
                                .Where(q => q.ApplicantId == id)
                                .Select(q => _Mapper.Map(q));

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<long> CreateTargetTaskAsync(TargetTaskDTO task) => await _DataProvider.CreateAsync(_Mapper.Map(task));

        /// <summary>
        /// Редактировать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<bool> EditTargetTaskAsync(TargetTaskDTO task) => await _DataProvider.EditAsync(_Mapper.Map(task));

        /// <summary>
        /// Удалить задание
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<bool> RemoveTargetTaskAsync(long id) => await _DataProvider.RemoveAsync<TargetTask>(id);

        /// <summary>
        /// Выполнить задание по идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<bool> ExecuteTargetTaskAsync(long id)
        {
            var task = await _DataProvider.GetByIdAsync<TargetTask>(id);

            // Установить статус - выполнено

            return await _DataProvider.EditAsync(task);
        }

        /// <summary>
        /// Отменить задание
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        public async Task<bool> AbortTargetTaskAsync(long id)
        {
            var task = await _DataProvider.GetByIdAsync<TargetTask>(id);

            // Установить статус - отменено

            return await _DataProvider.EditAsync(task);
        }
    }
}
