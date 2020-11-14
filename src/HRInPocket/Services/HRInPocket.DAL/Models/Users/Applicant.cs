using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    /// <summary>
    /// Соискатель
    /// </summary>
    public class Applicant : User
    {
        /// <summary>
        /// Выбранный тариф
        /// </summary>
        public Tarif Tarif { get; set; }

        /// <summary>
        /// Закрепленный системный менеджер
        /// </summary>
        public SystemManager SystemManager { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        public TargetTask TargetTask { get; set; }
        public string TargetTaskId { get; set; }
    }
}