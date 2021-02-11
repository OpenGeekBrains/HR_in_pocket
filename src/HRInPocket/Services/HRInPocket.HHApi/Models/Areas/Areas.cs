using System.Collections.Generic;


namespace HRInPocket.HHApi.Models.Areas
{
    public class Areas
    {
        /// <summary>
        /// Имя региона
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Id региона
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Id родительский регион
        /// </summary>
        public string parent_id { get; set; }

        /// <summary>
        /// Список дочерних регионов
        /// </summary>
        public IEnumerable<Areas> areas { get; set; }

    }
}
