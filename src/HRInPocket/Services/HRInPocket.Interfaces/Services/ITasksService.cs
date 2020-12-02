using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Interfaces.Services
{
    public interface ITasksService
    {
        Task<TargetTask> CreateTask(string UserId, decimal? Salary, string Position, bool RemoteWork, string Tags);

        Task<IEnumerable<TargetTask>> GetUserTasks(string UserId);
    }
}
