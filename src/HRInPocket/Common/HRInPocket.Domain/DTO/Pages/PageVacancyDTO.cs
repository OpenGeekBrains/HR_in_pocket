using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageVacancyDTO
    {
        public IEnumerable<VacancyDTO> Vacancies { get; set; } = new List<VacancyDTO>();
        public int? TotalCount { get; set; }
    }
}