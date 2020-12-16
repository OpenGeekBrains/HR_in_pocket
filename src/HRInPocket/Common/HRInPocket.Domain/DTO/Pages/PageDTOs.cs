using System.Collections.Generic;

using HRInPocket.Domain.DTO.Base;

namespace HRInPocket.Domain.DTO.Pages
{
    public class PageDTOs<TDto> where TDto: BaseDTO
    {
        public IEnumerable<TDto> Entities { get; set; } = new List<TDto>();
        public int TotalCount { get; set; }
    }
}