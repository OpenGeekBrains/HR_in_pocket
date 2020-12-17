using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.Entities.Profiles
{
    /// <summary>
    /// Профиль менеджера
    /// </summary>
    public class ManagerProfile : BaseProfile
    {
        /// <summary>
        /// Список закрепленных заданий
        /// </summary>
        public ICollection<TargetTask> TargetTasks { get; set; } = new List<TargetTask>();
    }
}