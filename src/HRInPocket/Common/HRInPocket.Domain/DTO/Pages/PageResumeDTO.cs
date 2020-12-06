using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageResumeDTO
    {
        public IEnumerable<ResumeDTO> Companies { get; set; } = new List<ResumeDTO>();
        public int? TotalCount { get; set; }
    }

}
