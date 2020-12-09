using System.Collections.Generic;
using System.Linq;

namespace HRInPocket.Domain.Models.JsonReturnModels
{
    public readonly struct ArrayContent
    {
        public ArrayContent(IEnumerable<object> content, bool result)
        {
            Content = content.ToArray();
            Result = result;
        }

        public readonly object[] Content;
        public readonly bool Result;
    }
}