using System.Collections.Generic;

namespace HRInPocket.HHApi.Models.Employers
{
    /// <summary>
    /// Класс вывода информации о работадателе при поиске
    /// </summary>
    public class FoundEmployerInfo
    {
        /// <summary>
        /// Идентификатор работодателя
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Название работодателя
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Url для получения полного описания работодателя
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Ссылка на описание работодателя на сайте
        /// </summary>
        public string alternate_url { get; set; }

        /// <summary>
        /// Url для получения поисковой выдачи с вакансиями данной компании
        /// </summary>
        public string vacancies_url { get; set; }

        /// <summary>
        /// Количество открытых вакансий у работодателя
        /// </summary>
        public int open_vacancies { get; set; }

        /// <summary>
        /// Логотипы компании
        /// </summary>
        public Dictionary<string, string> logo_urls { get; set; }
    }
}
