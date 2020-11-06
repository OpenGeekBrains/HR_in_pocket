using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Работодатель
    /// </summary>
    public class Employer : BaseUser
    {
        /// <summary>
        /// Список компаний
        /// </summary>
        public ICollection<Company> Companies { get; set; } = new HashSet<Company>();

        /// <summary>
        /// Менеджеры / представители работодателя
        /// </summary>
        public ICollection<CompanyManager> CompanyManagers { get; set; } = new HashSet<CompanyManager>();

        /// <summary>
        /// Список закрепленных системных менеджеров
        /// </summary>
        public ICollection<SystemManager> SystemManagers { get; set; } = new HashSet<SystemManager>();
    }
}