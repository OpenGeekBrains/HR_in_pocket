using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Mapper
{
    public class VacancyMapper : BaseMapper<Vacancy, VacancyDTO>, IMapper<Vacancy, VacancyDTO>
    {
        public VacancyDTO Map(Vacancy entity) => ToDTO(entity,
            cfg => cfg.CreateMap<Vacancy, VacancyDTO>());

        public Vacancy Map(VacancyDTO entity) => FromDTO(entity,
            cfg => cfg.CreateMap<VacancyDTO, Vacancy>());
    }
}
