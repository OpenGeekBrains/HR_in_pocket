using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Users;

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
        [Required]
        public Address FactAddress { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        public Address LegalAddress { get; set; }

        /// <summary>
        /// Список вакансий
        /// </summary>
        public ICollection<Vacancy> Vacancies { get; set; } = new HashSet<Vacancy>();
        
        //todo: добавить возможность добавления любых метаданных
    }
}