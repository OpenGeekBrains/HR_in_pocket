using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Компания / организация / ИП
    /// </summary>
    public class Company : NamedEntity
    {
        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [Required]
        public string Inn { get; set; } 

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public Address FactAddress { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        public Address LegalAddress { get; set; }

        /// <summary>
        /// Список вакансий
        /// </summary>
        public List<Vacancy> Vacancies { get; set; }
    }
}