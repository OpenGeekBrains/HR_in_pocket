using System.Threading.Tasks;
using HRInPocket.HHApi.Interfaces;
using HRInPocket.HHApi.Models.Employers;
using HRInPocket.HHApi.Services.Base;

namespace HRInPocket.HHApi.Services
{
    public class EmployersService : BaseClient, IEmployersServices
    {
        public async Task<FoundEmployersPage> FindEmplouer(FindOption findOption = null, PageOption pageOption = null)
        {
            string urlSearch = ApiUrl.employers + CreateFindString(findOption, pageOption);
            var result = await GetJsonAsync<FoundEmployersPage>(urlSearch);

            return result;
        }

        public async Task<EmployerInfo> GetEmployerInfoById(int employerId)
        {
            string urlEmployer = $"{ApiUrl.employers}/{employerId}";
            var result = await GetJsonAsync<EmployerInfo>(urlEmployer);

            return result;
        }

        private string CreateFindString(FindOption findOption, PageOption pageOption)
        {
            string result = "?";

            if (findOption == null && pageOption == null)
                return string.Empty;


            if (findOption != null)
            {
                if (findOption.text != null && findOption.text != string.Empty)
                    result += $"text={findOption.text}&";

                if (findOption.only_with_vacancies != string.Empty && findOption.only_with_vacancies == "true")
                    result += $"only_with_vacancies={findOption.only_with_vacancies}&";

                if (findOption.area != null)
                {
                    foreach (var item in findOption.area)
                    {
                        result += $"area={item}&";
                    }
                }

                if (findOption.type != null)
                {
                    foreach (var item in findOption.type)
                    {
                        result += $"type={item}&";
                    }
                }
            }

            if (pageOption != null)
            {
                if (pageOption.per_page > 0)
                    result += $"per_page={pageOption.per_page}&";

                if (pageOption.page > 0)
                    result += $"page={pageOption.page}&";
            }

            return result;
        }
    }
}
