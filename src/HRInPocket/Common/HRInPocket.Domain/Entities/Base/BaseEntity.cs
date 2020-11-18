using System;

namespace HRInPocket.Domain.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор ID
        /// </summary>
        public Guid Id { get; set; }
    }
}