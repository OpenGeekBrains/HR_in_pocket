using System;
using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Соискатель
    /// </summary>
    public class Applicant : NamedEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Список резюме
        /// </summary>
        public List<Resume> Resumes { get; set; }

        /// <summary>
        /// Список интересующих вакансий
        /// </summary>
        public List<Vacancy> SelectVacancies { get; set; }
    }
}