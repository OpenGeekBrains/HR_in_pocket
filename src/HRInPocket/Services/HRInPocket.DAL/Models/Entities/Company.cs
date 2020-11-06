using System.Collections.Generic;
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

        /// <summary>
        /// Работодатель-владелец компании
        /// </summary>
        public Employer Employer { get; set; }
    }
}