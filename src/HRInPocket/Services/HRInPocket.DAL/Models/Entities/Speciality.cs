using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
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