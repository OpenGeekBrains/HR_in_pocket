using System;
using System.Collections.Generic;

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
        /// Всегда меньше CompensationUp
        /// Если стоит 0 - то приставка ОТ, а сумму смотреть в CompensationUp
        /// </summary>
        public ulong CompensationDown { get; set; }

        /// <summary>
        /// Компенсация (зарплата) до N
        /// Всегда больше CompensationDown
        /// Если стоит 0 - то приставка ДО, а сумму смотреть в CompensationDown
        /// </summary>
        public ulong CompensationUp { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public string CurrencyCode { get; set; }

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
