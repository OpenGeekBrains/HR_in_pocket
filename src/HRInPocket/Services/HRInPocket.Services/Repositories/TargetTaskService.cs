using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services.Repository;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories
{
    public class TargetTaskService : ITargetTaskService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IDataRepository<TargetTask> _DataProvider;
        private readonly IMapper _Mapper;

        public TargetTaskService(IDataRepository<TargetTask> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        #region IRepository implementation
        /// <summary>
        /// Посмотреть все задания
        /// </summary>
        public async Task<PageDTOs<TargetTaskDTO>> GetAllAsync(Filter filter)
        {
            var query = await _DataProvider.GetQueryableAsync();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/

                query = query
                    .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                    .Take(filter.Pages.PageSize);
            }

            var count = await query.CountAsync();

            return new PageDTOs<TargetTaskDTO>
            {
                Entities = query.AsEnumerable().Select(_Mapper.Map<TargetTaskDTO>),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть информацию задания по его идентификатору
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<TargetTaskDTO> GetByIdAsync(Guid id) => _Mapper.Map<TargetTaskDTO>(await _DataProvider.GetByIdAsync(id));

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<Guid> CreateAsync(TargetTaskDTO task) => await _DataProvider.CreateAsync(_Mapper.Map<TargetTask>(task));

        /// <summary>
        /// Редактировать задание
        /// </summary>
        /// <param name="task">Модель представления задания</param>
        public async Task<bool> EditAsync(TargetTaskDTO task) => await _DataProvider.EditAsync(_Mapper.Map<TargetTask>(task));

        /// <summary>
        /// Удалить задание
        /// </summary>
        /// <param name="id">Идентифкатор задания</param>
        public async Task<bool> RemoveAsync(Guid id) => await _DataProvider.RemoveAsync(id); 
        #endregion



        /// <inheritdoc/>
        public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => 
            (await _DataProvider.GetQueryableAsync())
            .Where(q => q.ProfileId == id.ToString())
            .AsEnumerable()
            .Select(_Mapper.Map<TargetTaskDTO>);

        /// <inheritdoc/>
        public async Task<bool> ExecuteTargetTaskAsync(Guid id)
        {
            var task = await _DataProvider.GetByIdAsync(id);

            // Установить статус - выполнено

            return await _DataProvider.EditAsync(task);
        }

        /// <inheritdoc/>
        public async Task<bool> AbortTargetTaskAsync(Guid id)
        {
            var task = await _DataProvider.GetByIdAsync(id);

            // Установить статус - отменено

            return await _DataProvider.EditAsync(task);
        }
    }
}
