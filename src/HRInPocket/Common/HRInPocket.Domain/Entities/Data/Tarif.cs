﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRInPocket.Domain.Entities.Base;
using HRInPocket.Domain.Entities.Profiles;

namespace HRInPocket.Domain.Entities.Data
{
    /// <summary>
    /// Тфриф соискателя
    /// </summary>
    public class Tarif : NamedEntity
    {
        /// <summary>
        /// Количество приглашений
        /// </summary>
        [Required]
        public int Visits { get; set; }

        /// <summary>
        /// Стоимость тарифа
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public  decimal Price { get; set; }

        /// <summary>
        /// Описание тарифа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Коллекция соискателей с указанным тариформ
        /// </summary>
        public ICollection<ApplicantProfile> ApplicntApplicantProfiles { get; set; }
    }
}
