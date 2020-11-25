using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Users;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Профиль пользователя
    /// </summary>
    public class Profile : BaseEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }

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

        /// <summary>
        /// Связанный пользователь
        /// </summary>
        [Required]
        public User User { get; set; }
        public string UserId { get; set; }
    }
}