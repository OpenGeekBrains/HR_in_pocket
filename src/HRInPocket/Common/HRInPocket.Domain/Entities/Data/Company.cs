using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
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