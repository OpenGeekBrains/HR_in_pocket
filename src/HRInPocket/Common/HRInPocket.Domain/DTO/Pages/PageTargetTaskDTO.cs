using System.Collections.Generic;

namespace HRInPocket.Domain.DTO
{
    public class PageTargetTaskDTO
    {
        public IEnumerable<TargetTaskDTO> TargetTasks { get; set; } = new List<TargetTaskDTO>();
        public int? TotalCount { get; set; }
    }

}
