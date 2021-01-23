using System.Collections.Generic;

using HRInPocket.HHApi.Models.Areas;
using HRInPocket.HHApi.Models.Industries;

namespace HRInPocket.HHApi.Models.Employers
{
    public class EmployerInfo
    {
        /// <summary>
        /// Идентификатор компании
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Тип компании (прямой работодатель, кадровое агентство и т.п.)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Адрес сайта компании
        /// </summary>
        public string site_url { get; set; }

        /// <summary>
        /// Описание компании в виде строки с кодом HTML (без <script/> и <style/>)
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Брендированное описание компании в виде строки с кодом HTML (возможно наличие <script/> и <style/>)
        /// </summary>
        //  HTML адаптирован для мобильных устройств и корректно отображается без поддержки javascript. При этом:
        //      Контент тянется по ширине на 100% ширины контейнера и умещается без прокрутки в 300px.
        //      Контент рассчитан на то, что он будет вставлен в обвязку, в которую входит название, логотип, сайт и ссылка на вакансии компании.
        //      Изображения, которые могут встретиться в таком описании, адаптированы под retina-дисплеи.
        //      Размер шрифта не меньше 12px, размер межстрочного интервала не меньше 16px.
        public string branded_description { get; set; }

        /// <summary>
        /// Ссылка на поисковую выдачу вакансий данной компании.
        /// </summary>
        public string vacancies_url { get; set; }

        /// <summary>
        /// Количество открытых вакансий у работодателя
        /// </summary>
        public int open_vacancies { get; set; }

        /// <summary>
        /// Флаг, показывающий, прошла ли компания проверку на сайте.
        /// </summary>
        public bool trusted { get; set; }

        /// <summary>
        /// Ссылка на представление компании на сайте
        /// </summary>
        public string alternate_url { get; set; }

        /// <summary>
        /// Список интервью или пустой список, если интервью отсутствуют
        /// </summary>
        public IEnumerable<Interview> insider_interviews { get; set; }

        /// <summary>
        /// Логотипы компании
        /// </summary>
        public Dictionary<string, string> logo_urls { get; set; }

        /// <summary>
        /// Информация о регионе работодателя
        /// </summary>
        public Countries area { get; set; }

        /// <summary>
        /// Если работодатель добавлен в черный список, то вернется ['blacklisted'] иначе []
        /// </summary>
        public IEnumerable<string> relations { get; set; }

        /// <summary>
        /// Cписок отраслей компании
        /// </summary>
        public IEnumerable<IindustryInfo> industries { get; set; }
    }
}
