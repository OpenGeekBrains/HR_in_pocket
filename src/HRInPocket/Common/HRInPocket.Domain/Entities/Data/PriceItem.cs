using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Разовая платная услуга
    /// </summary>
    public class PriceItem : NamedEntity
    {
        /// <summary>
        /// Стоимость услуги
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}