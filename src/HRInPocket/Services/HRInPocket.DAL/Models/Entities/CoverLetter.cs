using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Users;

namespace HRInPocket.DAL.Models.Entities
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