using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Repository.Base;

namespace HRInPocket.Interfaces.Repository
{
    public interface ICompanyService : IDtoRepository<Company, CompanyDTO>
    {
        
    }
}