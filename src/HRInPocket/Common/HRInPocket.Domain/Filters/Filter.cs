using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Domain.Filters
{
    public class Filter
    {
        public PageModel Pages { get; set; }

        public IQueryable<T> Paging<T>(IQueryable<T> query)
        {
            var (page, pageSize) = (Pages.PageNumber, Pages.PageSize);
            var result = query.Skip(page * pageSize).Take(pageSize);
            return result;
        }

        public virtual async Task<(IQueryable<T> Query, int TotalCount)> Paging<T>(IQueryable<T> query, int Page, int PageSize)
        {
            var total_count = await query.CountAsync();
            var result = query.Skip(Page * PageSize).Take(PageSize);
            return (result, total_count);
        }
        
    }
}