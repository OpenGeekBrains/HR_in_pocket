using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

using HRInPocket.Parsing.hh.ru.Models.Base;

namespace HRInPocket.Parsing.hh.ru.Models.Entites
{    
    public class Vacancy
    {
        /// <summary>
        /// Название вакансии
        /// </summary>
        public VacancyName Name { get; set; }

        /// <summary>
        /// Компания, разместившая вакансию
        /// </summary>
        public Company Company { get; set; }

        public IEnumerable<BaseUnit> Meta {get;set;}        
        /// <summary>
        /// Адрес
        /// </summary>
        public string VacancyAddress { get; set; }
        /// <summary>
        /// Описание Вакансии
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Краткое описание вакансии
        /// </summary>
        public string ShortDescription { get; set; }
        
        /// <summary>
        /// Компенсация (зарплата) от N
        /// </summary>
        public ulong CompensationDown { get; set; }

        /// <summary>
        /// Компенсация (зарплата) до N
        /// </summary>
        public ulong CompensationUp { get; set; }

        /// <summary>
        /// Код Валюты в ISO 4217
        /// </summary>
        public ushort CurrencyCode { get; set; }

        /// <summary>
        /// Прификс (от,до) для компенсации
        /// </summary>
        public string PrefixCompensation { get; set; }

        /// <summary>
        /// Дата размещения вакансии
        /// </summary>
        public DateTime Date { get; set; }

    }
}
