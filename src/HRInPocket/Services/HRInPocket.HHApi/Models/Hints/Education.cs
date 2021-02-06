using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRInPocket.HHApi.Models.Hints
{
    public class Education
    {
        public string acronym { get; set; } //"МГТУ им. Н.Э. Баумана"

        public string text { get; set; } //"Московский государственный технический университет им. Н.Э. Баумана, Москва"

        public string synonyms { get; set; } //"бывш. МВТУ им. Н.Э. Баумана"

        public string id { get; set; }

        public EdocationArea area { get; set; }
    }

    public class EdocationArea
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
