using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
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