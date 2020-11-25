using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PagePriceItemDTO
    {
        public IEnumerable<PriceItemDTO> Companies { get; set; } = new List<PriceItemDTO>();
        public int TotalCount { get; set; }
    }
}
