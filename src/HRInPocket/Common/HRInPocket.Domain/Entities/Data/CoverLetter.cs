using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Users;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Сопроводительное письмо
    /// </summary>
    public class CoverLetter : BaseEntity
    {
        /// <summary>
        /// Соискатель-владелец сопроводительного письма
        /// </summary>
        public Applicant Applicant { get; set; }
        
        /// <summary>
        /// Поля сопроводительных писем
        /// </summary>
        public ICollection<CoverLetterValue> Values { get; set; } = new HashSet<CoverLetterValue>();
    }
}