using HRInPocket.HHApi.Interfaces;
using HRInPocket.HHApi.Models.Areas;
using HRInPocket.HHApi.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HRInPocket.HHApi.Services
{
    public class AreasService : BaseClient, IAreasServices
    {

        public async Task<IEnumerable<Countries>> GetCountries() => await GetJsonAsync<IEnumerable<Countries>>(ApiUrl.Areas.countries);

        public async Task<IEnumerable<Areas>> GetAreas() => await GetJsonAsync<IEnumerable<Areas>>(ApiUrl.Areas.areas);

        public async Task<Areas> GetAreasStartFromId(string startId) => await GetJsonAsync<Areas>(ApiUrl.Areas.areas+"/"+startId);
        
    }
}
