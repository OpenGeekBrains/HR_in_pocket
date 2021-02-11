using System.Collections.Generic;

namespace HRInPocket.HHApi.Models.Employers
{
    public class FoundEmployersPage
    {
        /// <summary>
        /// Количество отображаемых на страницу элементов
        /// </summary>
        public int per_page { get; set; }

        /// <summary>
        /// Номер отображаемой страницы
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Количество страниц с данными
        /// </summary>
        public int pages { get; set; }

        /// <summary>
        /// Количество работодателей, найденных по переданному поисковому критерию
        /// </summary>
        public int found { get; set; }

        /// <summary>
        /// Найденные работодатели (максимум per_page элементов)
        /// </summary>
        public IEnumerable<FoundEmployerInfo> items { get; set; }
    }
}
