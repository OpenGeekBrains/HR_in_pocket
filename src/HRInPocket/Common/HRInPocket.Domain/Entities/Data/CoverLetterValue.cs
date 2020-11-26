using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Значение поля сопроводительное письмо
    /// </summary>
    public class CoverLetterValue : NamedEntity
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}