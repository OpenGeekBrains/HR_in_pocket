using System;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Запись истории задания
    /// </summary>
    public class TaskHistoryRecord : BaseEntity
    {
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Новое состояние задачи
        /// </summary>
        public TaskState State { get; set; }

        /// <summary>
        /// внешний ключ на журнал
        /// </summary>
        public Guid TaskHistoryId { get; set; }
        public TaskHistory TaskHistory { get; set; }
    }
}