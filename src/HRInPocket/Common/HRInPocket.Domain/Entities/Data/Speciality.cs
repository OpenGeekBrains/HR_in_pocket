using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Профессия
    /// </summary>
    public class Speciality : NamedEntity
    {
        /// <summary>
        /// Категория / род деятельности
        /// </summary>
        public ActivityCategory ActivityCategory { get; set; }
    }
}