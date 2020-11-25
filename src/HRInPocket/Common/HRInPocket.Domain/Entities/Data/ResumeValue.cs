using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Значение поля резюме
    /// </summary>
    public class ResumeValue : NamedEntity
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}