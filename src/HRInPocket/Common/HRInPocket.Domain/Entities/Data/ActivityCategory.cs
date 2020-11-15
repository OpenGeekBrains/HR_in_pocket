using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Вид деятельности
    /// </summary>
    public class ActivityCategory : NamedEntity
    {
        /// <summary>
        /// Список специальностей
        /// </summary>
        public ICollection<Speciality> Specialties { get; set; } = new HashSet<Speciality>();
    }
}