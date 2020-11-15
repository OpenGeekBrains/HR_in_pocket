using System;
using System.Collections.Generic;

namespace HRInPocket.WPF.ViewModels
{
    class Vacancy
    {
        public VacancyName Name { get; set; }

        public IEnumerable<BaseUnit> Meta {get;set;}        

        public string Description { get; set; }

        public uint Price { get; set; }

        public DateTime Date { get; set; }

    }
}
