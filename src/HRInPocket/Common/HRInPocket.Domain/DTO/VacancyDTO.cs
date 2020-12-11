
using HRInPocket.Domain.DTO.Base;

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
}
