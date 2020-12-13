using System;
using System.Collections.Generic;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.Entities.Profiles
{
    /// <summary>
    /// Профиль пользователя
    /// </summary>
    public class ApplicantProfile : BaseUser
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; } = default;

        /// <summary>
        /// Список резюме
        /// </summary>
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>();

        /// <summary>
        /// Список сопроводительных писем
        /// </summary>
        public  ICollection<CoverLetter> CoverLetters { get; set; } = new List<CoverLetter>();

        /// <summary>
        /// Список интересующих специальностей
        /// </summary>
        public ICollection<Speciality> Speciality { get; set; } = new List<Speciality>();
    }
}