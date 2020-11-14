using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

using HRInPocket.Parsing.hh.ru.Models.Base;

namespace HRInPocket.Parsing.hh.ru.Models.Entites
{
    public class Vacancy
    {
        public VacancyName Name { get; set; }

        public Company Company { get; set; }
        public IEnumerable<BaseUnit> Meta {get;set;}        

        public string Description { get; set; }

        public uint Compensation { get; set; }

        public DateTime Date { get; set; }

    }
}
