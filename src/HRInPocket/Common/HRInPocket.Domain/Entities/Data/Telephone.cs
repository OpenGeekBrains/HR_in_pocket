using System;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public class Telephone : NamedEntity
    {
        public string Number { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public BaseProfile BaseProfile { get; set; }
        public Guid BaseProfileId { get; set; }
    }
}