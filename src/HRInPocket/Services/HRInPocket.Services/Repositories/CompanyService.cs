using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;
using HRInPocket.Interfaces.Repository.Base;
using HRInPocket.Services.Repositories.Base;

namespace HRInPocket.Services.Repositories
{
    public class CompanyService : DtoRepository<Company,CompanyDTO>, ICompanyService
    {
        public CompanyService(IDataRepository<Company> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
        }
    }
}
