using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "decimal(18,2)")]
        public  decimal Price { get; set; }

        /// <summary>
        /// Описание тарифа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Коллекция соискателей с указанным тариформ
        /// </summary>
        public ICollection<Applicant> Applicants { get; set; } = new HashSet<Applicant>();
    }
}
