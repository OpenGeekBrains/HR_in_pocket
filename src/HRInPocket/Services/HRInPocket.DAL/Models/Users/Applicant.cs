using System;
using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Соискатель
    /// </summary>
    public class Applicant : BaseUser
    {
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
        public ICollection<Resume> Resumes { get; set; } = new HashSet<Resume>();

        /// <summary>
        /// Список интересующих специальностей
        /// </summary>
        public ICollection<Speciality> Speciality { get; set; } = new HashSet<Speciality>();

        /// <summary>
        /// Закрепленный системный менеджер
        /// </summary>
        public SystemManager SystemManager { get; set; }
    }
}