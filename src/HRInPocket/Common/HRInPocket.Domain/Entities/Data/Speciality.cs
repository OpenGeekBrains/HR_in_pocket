using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Profiles;

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

        /// <summary>
        /// Список соискателей интересующихся данной специальностью
        /// </summary>
        public ICollection<ApplicantProfile> Applicants { get; set; } = new List<ApplicantProfile>();
    }
}