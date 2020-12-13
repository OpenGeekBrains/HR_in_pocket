using System;
using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Profiles;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Сопроводительное письмо
    /// </summary>
    public class CoverLetter : BaseEntity
    {
        /// <summary>
        /// Поля сопроводительных писем
        /// </summary>
        public ICollection<CoverLetterValue> Values { get; set; } = new HashSet<CoverLetterValue>();

        /// <summary>
        /// Соискатель-владелец сопроводительного письма
        /// </summary>
        public ApplicantProfile ApplicantProfile { get; set; }
        public Guid ApplicantProfileId { get; set; }
    }
}