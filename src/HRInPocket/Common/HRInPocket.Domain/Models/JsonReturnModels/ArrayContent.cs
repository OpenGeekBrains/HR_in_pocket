using System.Collections.Generic;
using System.Linq;

namespace HRInPocket.Domain.Models.JsonReturnModels
{
    public struct ArrayContent
    {
        public ArrayContent(IEnumerable<object> content, bool result)
        {
            Content = content.ToArray();
            Result = result;
        }

        public object[] Content { get; set; }
        public bool Result { get; set; }
    }
}