using System.Threading.Tasks;
using HRInPocket.HHApi.Models.Employers;

namespace HRInPocket.HHApi.Interfaces
{
    public interface IEmployersServices
    {
        /// <summary>
        /// Поиск работадателей/компании
        /// </summary>
        /// <param name="findOption">Опции поиска</param>
        /// <param name="pageOption">Опции страницы вывода найденой информации</param>
        /// <returns></returns>
        public Task<FoundEmployersPage> FindEmplouer(FindOption findOption, PageOption pageOption);

        /// <summary>
        /// Читаем информацию о работадателе/компании
        /// </summary>
        /// <param name="employerId">Id работадателя/компании</param>
        /// <returns></returns>
        public Task<EmployerInfo> GetEmployerInfoById(int employerId);
    }
}
