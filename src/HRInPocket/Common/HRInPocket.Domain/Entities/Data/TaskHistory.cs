using System;
using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// История задания
    /// </summary>
    public class TaskHistory : BaseEntity
    {
        /// <summary>
        /// Журнал записей
        /// </summary>
        public ICollection<TaskHistoryRecord> TaskHistoryRecords { get; set; } = new List<TaskHistoryRecord>();

        /// <summary>
        /// Внешний ключ на задание
        /// </summary>
        public Guid TargetTaskId { get; set; }
        public TargetTask TargetTask { get; set; }
    }
}