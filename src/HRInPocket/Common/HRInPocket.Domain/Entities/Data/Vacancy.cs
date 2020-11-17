using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
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