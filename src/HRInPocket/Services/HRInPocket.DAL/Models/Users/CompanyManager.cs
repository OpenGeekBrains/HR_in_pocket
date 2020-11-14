using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Менеджер организации
    /// </summary>
    public class CompanyManager : BaseEntity
    {
        /// <summary>
        /// Компания, представляемая менеджером
        /// </summary>
        public Company Company { get; set; }
    }
}