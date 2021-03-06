﻿using System;

namespace HRInPocket.Domain.DTO.Base
{
    /// <summary>
    /// Базовый класс пользователя системы
    /// </summary>
    public abstract class BaseUserDTO : NamedDTO
    {
        /// <summary>
        /// Идентификатор GUID </summary>
        public new Guid Id { get; set; }

        /// <summary>
        /// Фамилия </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Подтвержденный адрес электронной почты </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Дата регистрации </summary>
        public DateTime RegisterDate { get; set; }
    }
}
