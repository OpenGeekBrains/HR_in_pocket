using System.Collections.Generic;
using System.Threading.Tasks;
using HRInPocket.HHApi.Models.Areas;

namespace HRInPocket.HHApi.Interfaces
{
    interface IAreasServices
    {
        /// <summary>
        /// Читаем список стран
        /// </summary>
        /// <returns>Список стран из БД hh.ru</returns>
        public Task<IEnumerable<Countries>> GetCountries();

        /// <summary>
        /// Читаем дерево всех регионов
        /// </summary>
        /// <returns>Дерево регионов из БД hh.ru</returns>
        public Task<IEnumerable<Areas>> GetAreas();

        /// <summary>
        /// Читаем дерево регионов начиная с указанного Id
        /// </summary>
        /// <param name="startId">Id региона с которого начинать дерево</param>
        /// <returns></returns>
        public Task<Areas> GetAreasStartFromId(string startId);
    }
}
