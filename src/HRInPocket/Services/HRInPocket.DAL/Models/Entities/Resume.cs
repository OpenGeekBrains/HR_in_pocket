using System.Collections;
using System.Collections.Generic;

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

        /// <summary>
        /// Поля резюме
        /// </summary>
        public ICollection<ResumeValue> Values { get; set; } = new HashSet<ResumeValue>();
    }
}