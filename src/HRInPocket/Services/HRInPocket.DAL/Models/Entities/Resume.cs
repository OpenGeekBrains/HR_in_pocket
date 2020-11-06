using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Users;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Резюме соискателя
    /// </summary>
    public class Resume : BaseEntity
    {
        /// <summary>
        /// Соискатель-владелец резюме
        /// </summary>
        public Applicant Applicant { get; set; }
    }
}