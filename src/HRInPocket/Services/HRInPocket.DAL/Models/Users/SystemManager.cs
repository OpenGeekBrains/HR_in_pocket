using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Менеджер системы
    /// </summary>
    public class SystemManager : BaseUser
    {
        /// <summary>
        /// Список закрепленных соискателей
        /// </summary>
        public List<Applicant> Applicants { get; set; }

        /// <summary>
        /// Список закрепленных работодателей
        /// </summary>
        public List<Employer> Employers { get; set; }
    }
}