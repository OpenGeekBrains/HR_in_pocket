using System;

using HRInPocket.Parsing.hh.ru.Models.Base;

namespace HRInPocket.Parsing.hh.ru.Models
{
    /// <summary>Вакансия</summary>
    public class Vacancy : BaseUnit
    {
        /// <summary>Компания, разместившая вакансию</summary>
        public Company Company { get; set; }

        /// <summary>Адрес</summary>
        public string VacancyAddress { get; set; }
        
        /// <summary>Описание вакансии</summary>
        public string Description { get; set; }

        /// <summary>Краткое описание вакансии</summary>
        public string ShortDescription { get; set; }

        /// <summary>Компенсация (зарплата) от N. Всегда меньше CompensationUp. Если стоит 0 - то приставка ОТ, а сумму смотреть в CompensationUp</summary>
        public ulong CompensationDown { get; set; }

        /// <summary>Компенсация (зарплата) до N. Всегда больше CompensationDown. Если стоит 0 - то приставка ДО, а сумму смотреть в CompensationDown</summary>
        public ulong CompensationUp { get; set; }

        /// <summary>Валюта</summary>
        public string CurrencyCode { get; set; }

        /// <summary>Прификс (от,до) для компенсации</summary>
        public string PrefixCompensation { get; set; }

        /// <summary>Дата размещения вакансии</summary>
        public DateTime Date { get; set; }

    }
}
