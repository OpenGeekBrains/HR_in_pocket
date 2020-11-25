using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageServiceDTO
    {
        public IEnumerable<ServiceDTO> Companies { get; set; } = new List<ServiceDTO>();
        public int TotalCount { get; set; }
    }
}
