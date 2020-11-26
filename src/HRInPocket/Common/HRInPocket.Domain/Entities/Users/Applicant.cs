using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.Entities.Users
{
    /// <summary>
    /// Соискатель
    /// </summary>
    public class Applicant : User
    {
        /// <summary>
        /// Выбранный тариф
        /// </summary>
        public Tarif Tarif { get; set; }

        /// <summary>
        /// Закрепленный системный менеджер
        /// </summary>
        public SystemManager SystemManager { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        public TargetTask TargetTask { get; set; }
        public string TargetTaskId { get; set; }
    }
}