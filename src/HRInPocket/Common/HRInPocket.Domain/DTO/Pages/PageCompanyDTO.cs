using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageCompanyDTO
    {
        public IEnumerable<CompanyDTO> Items { get; set; } = new List<CompanyDTO>();
        public int? TotalCount { get; set; }
    }

}
