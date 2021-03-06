﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HRInPocket.Domain.DTO.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// Идентификатор ID
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
