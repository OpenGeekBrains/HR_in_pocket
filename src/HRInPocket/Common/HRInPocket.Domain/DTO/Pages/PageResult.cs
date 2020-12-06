using System;
using System.Collections.Generic;
using System.Text;

namespace HRInPocket.Domain.DTO.Pages
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int? TotalCount { get; set; }
    }
}
