using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Вакансия
    /// </summary>
    public class Vacancy : BaseEntity
    {
        /// <summary>
        /// Специальность
        /// </summary>
        public Speciality Specialty { get; set; }

        /// <summary>
        /// Минимальная зарплата
        /// </summary>
        public int MinSalary { get; set; }

        /// <summary>
        /// Максимальная зарплата
        /// </summary>
        public int MaxSalary { get; set; }

        /// <summary>
        /// Компания-владелец вакансии
        /// </summary>
        public Company Company { get; set; }
    }
}