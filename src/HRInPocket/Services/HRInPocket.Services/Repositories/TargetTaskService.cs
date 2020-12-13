using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;
using HRInPocket.Interfaces.Repository.Base;
using HRInPocket.Services.Repositories.Base;

namespace HRInPocket.Services.Repositories
{
    //public class TargetTaskService : DtoRepository<TargetTask, TargetTaskDTO>, ITargetTaskService
    //{
    //    public TargetTaskService(IDataRepository<TargetTask> dataProvider, IMapper mapper) : base(dataProvider, mapper)
    //    {
    //    }


    //    /// <inheritdoc/>
    //    public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => 
    //        (await _DataProvider.GetQueryableAsync())
    //        .Where(q => q.ProfileId == id.ToString())
    //        .AsEnumerable()
    //        .Select(_Mapper.Map<TargetTaskDTO>);

    //    /// <inheritdoc/>
    //    public async Task<bool> ExecuteTargetTaskAsync(Guid id)
    //    {
    //        var task = await _DataProvider.GetByIdAsync(id);

    //        // Установить статус - выполнено

    //        return await _DataProvider.EditAsync(task);
    //    }

    //    /// <inheritdoc/>
    //    public async Task<bool> AbortTargetTaskAsync(Guid id)
    //    {
    //        var task = await _DataProvider.GetByIdAsync(id);

    //        // Установить статус - отменено

    //        return await _DataProvider.EditAsync(task);
    //    }
    //}
}
