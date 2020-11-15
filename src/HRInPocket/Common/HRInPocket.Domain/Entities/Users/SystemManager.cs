using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Users
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