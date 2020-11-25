using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Mapper
{
    public class CompanyMapper : BaseMapper<Company, CompanyDTO>, IMapper<Company, CompanyDTO>
    {
        public CompanyDTO Map(Company entity) => ToDTO(entity,
            cfg => cfg.CreateMap<Company, CompanyDTO>());

        public Company Map(CompanyDTO entity) => FromDTO(entity,
            cfg => cfg.CreateMap<CompanyDTO, Company>());
    }
}
