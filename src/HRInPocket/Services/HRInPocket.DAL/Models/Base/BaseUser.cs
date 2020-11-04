using System;
using System.ComponentModel.DataAnnotations;

namespace HRInPocket.DAL.Models.Base
{
    /// <summary>
    /// Базовый класс пользователя системы
    /// </summary>
    public abstract class BaseUser : NamedEntity
    {
        /// <summary>
        /// Подтвержденный адрес электронной почты
        /// </summary>
        [Required]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegisterDate { get; set; }
    }
}
