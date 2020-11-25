using HRInPocket.Domain.Entities.Data;
using HRInPocket.ViewModels.MakeTask;

using Profile = AutoMapper.Profile;

namespace HRInPocket.Infrastructure.Profiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<CreateTaskViewModel, TargetTask>()
                .ForMember(model => model.Speciality, opt => opt.MapFrom(view => view.Position))
                .ForMember(model => model.Salary, opt => opt.MapFrom(view => view.Salary ?? default ))
                .ForMember(model => model.ResumeLink, opt => opt.MapFrom(view => view.ResumeUrl))
                .ReverseMap();
        }
    }
}
