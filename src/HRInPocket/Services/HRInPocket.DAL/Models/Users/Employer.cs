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
        public List<Company> Companies { get; set; }

        /// <summary>
        /// Менеджеры / представители работодателя
        /// </summary>
        public List<CompanyManager> Managers { get; set; }
    }
}