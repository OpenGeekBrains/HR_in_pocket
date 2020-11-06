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
        public List<Resume> Resumes { get; set; }

        /// <summary>
        /// Список интересующих специальностей
        /// </summary>
        public List<Speciality> Speciality { get; set; }

        /// <summary>
        /// Закрепленный системный менеджер
        /// </summary>
        public SystemManager SystemManager { get; set; }
    }
}