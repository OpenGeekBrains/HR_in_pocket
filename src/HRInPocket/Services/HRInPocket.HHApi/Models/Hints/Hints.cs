using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRInPocket.HHApi.Models.Hints
{
    public class Hints<T>
    {
        public IEnumerable<T> items { get; set; }
    }
}
