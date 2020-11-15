using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Users;

namespace HRInPocket.Domain.Entities.Data
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

        /// <summary>
        /// Поля резюме
        /// </summary>
        public ICollection<ResumeValue> Values { get; set; } = new HashSet<ResumeValue>();
    }
}