using System.Collections.Generic;

namespace HRInPocket.Infrastructure.Models.JsonReturnModels
{
    public record ArrayContent(IEnumerable<object> content, bool result);
}