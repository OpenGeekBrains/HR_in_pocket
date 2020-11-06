using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
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