using System;

namespace HRInPocket.DAL.Models.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор GUID
        /// </summary>
        public Guid Id { get; set; }
    }
}