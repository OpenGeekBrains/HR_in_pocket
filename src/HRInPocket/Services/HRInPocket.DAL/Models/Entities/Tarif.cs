using System.ComponentModel.DataAnnotations;
using HRInPocket.DAL.Models.Base;

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
    }
}
