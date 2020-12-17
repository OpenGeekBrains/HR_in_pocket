using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.Entities.Base
{
    public abstract class BaseProfile : BaseEntity
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
        /// Список телефонных номеров
        /// </summary>
        public ICollection<Telephone> TelNumbers { get; set; } = new List<Telephone>();

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Фото / аватар
        /// </summary>
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }
    }
}
