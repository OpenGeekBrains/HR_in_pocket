using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
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