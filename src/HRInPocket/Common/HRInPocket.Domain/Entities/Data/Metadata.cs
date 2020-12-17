using System;
using HRInPocket.Domain.Entities.Base;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Класс метаданных
    /// </summary>
    public class Metadata : NamedEntity
    {
        /// <summary>
        /// Объект метаданных
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Сслка на базовую сущность
        /// </summary>
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}