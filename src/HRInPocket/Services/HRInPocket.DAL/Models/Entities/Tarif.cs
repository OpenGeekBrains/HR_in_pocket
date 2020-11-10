﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Users;

namespace HRInPocket.DAL.Models.Entities
{
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
        public  decimal Price { get; set; }

        /// <summary>
        /// Описание тарифа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Коллекция соискателей с указанным тариформ
        /// </summary>
        public Collection<Applicant> Applicants { get; set; }
    }
}
