using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRInPocket.HHApi.Models.Hints
{
    public class Companies
    {
        public string text { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public IEnumerable<Industries> industries { get; set; }
        public Dictionary<string,string> logo_urls { get; set; }
    }

    public class Industries
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
