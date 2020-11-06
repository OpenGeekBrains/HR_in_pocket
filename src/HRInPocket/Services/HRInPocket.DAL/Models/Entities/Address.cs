using HRInPocket.DAL.Models.Base;

namespace HRInPocket.DAL.Models.Entities
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address : BaseEntity
    {
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }


        /// <summary>
        /// Дом / строение
        /// </summary>
        public string Building { get; set; }
    }
}