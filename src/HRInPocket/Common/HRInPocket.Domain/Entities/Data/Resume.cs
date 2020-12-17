using System;
using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Profiles;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Резюме соискателя
    /// </summary>
    public class Resume : BaseEntity
    {
        /// <summary>
        /// Поля резюме
        /// </summary>
        public ICollection<ResumeValue> Values { get; set; }

        /// <summary>
        /// Соискатель-владелец резюме
        /// </summary>
        public ApplicantProfile ApplicantProfile { get; set; }
        public Guid ApplicantProfileId { get; set; }

    }
}