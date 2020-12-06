using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageCompanyDTO
    {
        public IEnumerable<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();
        public int? TotalCount { get; set; }
    }

}
