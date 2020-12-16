
using System.Collections.Generic;
using HRInPocket.Domain.DTO.Base;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.DTO
{
    public class VacancyDTO : BaseDTO
    {
        /// <summary>
        /// Минимальная зарплата
        /// </summary>
        public int MinSalary { get; set; }

        /// <summary>
        /// Максимальная зарплата
        /// </summary>
        public int MaxSalary { get; set; }
    }

    public class VacancyCollection
    {
        public List<Vacancy> Vacancies { get; set; }
    }
}
