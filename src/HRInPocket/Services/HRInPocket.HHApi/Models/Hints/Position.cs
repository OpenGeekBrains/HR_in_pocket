using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRInPocket.HHApi.Models.Hints
{
    public class Position
    {
        public string text { get; set; }

        public string id { get; set; }

        public IEnumerable<PositionSpec> specializations { get; set; }
    }

    public class PositionSpec
    {
        public string id { get; set; }

        public string name { get; set; }

        public string profarea_id { get; set; }

        public string profarea_name { get; set; }
    }
}
