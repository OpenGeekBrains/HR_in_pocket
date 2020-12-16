using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Domain.AutoMapperProfiles
{
    public class MappingDTOProfile : AutoMapper.Profile
    {
        public MappingDTOProfile()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Resume, ResumeDTO>().ReverseMap();
            CreateMap<TargetTask, TargetTaskDTO>().ReverseMap();
            CreateMap<Tarif, TarifDTO>().ReverseMap();
            CreateMap<Vacancy, VacancyDTO>().ReverseMap();
        }
    }
}
