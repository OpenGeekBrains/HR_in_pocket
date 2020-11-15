using System.Collections.Generic;

namespace HRInPocket.Domain.Entities.Users
{
    /// <summary>
    /// Менеджер системы
    /// </summary>
    public class SystemManager : User
    {
        /// <summary>
        /// Список закрепленных соискателей
        /// </summary>
        public ICollection<Applicant> Applicants { get; set; } = new HashSet<Applicant>();
    }
}