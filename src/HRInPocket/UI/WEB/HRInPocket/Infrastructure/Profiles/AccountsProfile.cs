using System;
using System.Linq;

using AutoMapper;
using HRInPocket.ViewModels.Account;
using userData = HRInPocket.Domain.Entities.Data;

namespace HRInPocket.Infrastructure.Profiles
{
    //public class AccountsProfile : Profile
    //{
    //    public AccountsProfile()
    //    {
    //        CreateMap<userData::Profile, UserProfileViewModel>()
    //            .ForMember(viewModel=>viewModel.Specialities,opt=>opt.MapFrom(model=>model.Speciality.ToList()))
    //            .ForMember(viewModel=>viewModel.Resumes,opt=>opt.MapFrom(model=>model.Resumes.ToList()))
    //            .ForMember(viewModel=>viewModel.CoverLetters,opt=>opt.MapFrom(model=>model.CoverLetters.ToList()))
    //            .ForMember(viewModel=>viewModel.Birthday, opt=>opt.MapFrom(model=>
    //                model.Birthday == default ? (int?) null : GetFullYears(model.Birthday, DateTime.Now)))
    //            .ReverseMap();
    //    }

    //    private static int GetFullYears(DateTime dt1, DateTime dt2)
    //    {
    //        if (dt2.Year <= dt1.Year)
    //            return 0;
    //        var n = dt2.Year - dt1.Year;
    //        if (dt1.DayOfYear >= dt2.DayOfYear)
    //            --n;
    //        return n;
    //    }
    //}
}