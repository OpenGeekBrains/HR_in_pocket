using AutoMapper;
using HRInPocket.ViewModels.Account;
using userData = HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Infrastructure.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<userData::Profile, UserProfileViewModel>()
                .ForMember(viewModel=>viewModel.Specialities,opt=>opt.MapFrom(model=>model.Speciality))
                // TODO: Bad Map - rework model(UserName is Nickname, it's not FirstName)
                .ForMember(viewModel=>viewModel.FirstName, opt=>opt.MapFrom(model=>model.User.UserName))
                .ReverseMap();
        }
    }
}