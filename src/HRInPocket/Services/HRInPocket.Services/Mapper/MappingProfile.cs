
using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Services.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Resume, ResumeDTO>().ReverseMap();
            CreateMap<TargetTask, TargetTaskDTO>().ReverseMap();
            CreateMap<Tarif, TarifDTO>().ReverseMap();
            CreateMap<Vacancy, VacancyDTO>().ReverseMap();
        }
    }
}
