using System.Collections.Generic;

namespace HRInPocket.HHApi.Models.Employers
{
    public class FindOption
    {
        /// <summary>
        /// Текстовое поле, переданное значение ищется в названии и описании компании
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// идентификатор региона работодателя, множественный параметр
        /// </summary>
        public List<string> area { get; set; }

        /// <summary>
        /// Типы работодателей, множественный параметр.
        /// </summary>
        public List<string> type { get; set; }

        /// <summary>
        /// Возвращать только работодателей у которых есть в данный момент открытые вакансии. Значения true или false.
        /// По умолчанию false
        /// </summary>
        public string only_with_vacancies { get; set; }
    }
}
