using System;
using System.Collections.Generic;
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
    public class TargetTaskService : ITargetTaskService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository<TargetTask> _DataProvider;
        private readonly IMapper _Mapper;

        public TargetTaskService(IDataRepository<TargetTask> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        /// <summary>
        /// Посомтреть все задания
        /// </summary>
        public async Task<PageTargetTaskDTO> GetAllTargetTasksAsync(TargetTaskFilter filter = null)
        {
            var query = _DataProvider.GetQueryable();
            int count = 0;
            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/

               count = await query.CountAsync();

            query = query
                .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                .Take(filter.Pages.PageSize);
            }

            return new PageTargetTaskDTO
            {
                Items = query.Select(q => _Mapper.Map<TargetTaskDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть информацию задания по его идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<TargetTaskDTO> GetTargetTaskByIdAsync(Guid id) => _Mapper.Map<TargetTaskDTO>((await _DataProvider.GetByIdAsync(id)));

        /// <summary>
        /// Посомтреть все задания пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => 
            (await _DataProvider.GetQueryableAsync())
                                .Where(q => q.ProfileId == id.ToString())
                                .Select(q => _Mapper.Map<TargetTaskDTO>(q));

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<Guid> CreateTargetTaskAsync(TargetTaskDTO task) => await _DataProvider.CreateAsync(_Mapper.Map<TargetTask>(task));

        /// <summary>
        /// Редактировать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<bool> EditTargetTaskAsync(TargetTaskDTO task) => await _DataProvider.EditAsync(_Mapper.Map<TargetTask>(task));

        /// <summary>
        /// Удалить задание
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<bool> RemoveTargetTaskAsync(Guid id) => await _DataProvider.RemoveAsync(id);

        /// <summary>
        /// Выполнить задание по идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<bool> ExecuteTargetTaskAsync(Guid id)
        {
            var task = await _DataProvider.GetByIdAsync(id);

            // Установить статус - выполнено

            return await _DataProvider.EditAsync(task);
        }

        /// <summary>
        /// Отменить задание
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        public async Task<bool> AbortTargetTaskAsync(Guid id)
        {
            var task = await _DataProvider.GetByIdAsync(id);

            // Установить статус - отменено

            return await _DataProvider.EditAsync(task);
        }
    }
}
