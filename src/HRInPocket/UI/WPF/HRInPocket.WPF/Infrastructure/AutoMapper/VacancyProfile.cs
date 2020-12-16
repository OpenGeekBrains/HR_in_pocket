using HRInPocket.Domain.Entities.Data;

using Profile = AutoMapper.Profile;

namespace HRInPocket.WPF.Infrastructure.AutoMapper
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, Parsing.hh.ru.Models.Vacancy>()
                .ForMember(v => v.Name, hh => hh.MapFrom(v => v.Specialty.Name))
                .ForMember(v => v.VacancyAddress, hh => hh.MapFrom(v => v.Company.LegalAddress))
                .ForMember(v => v.CompensationUp, hh => hh.MapFrom(v => v.MaxSalary))
                .ForMember(v => v.CompensationDown, hh => hh.MapFrom(v => v.MinSalary))
                .ReverseMap();

            CreateMap<Company, Parsing.hh.ru.Models.Company>()
               .ForMember(v => v.Name, hh => hh.MapFrom(v => v.Name))
               .ReverseMap();
        }
    }
}
