using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.ViewModels.Account;

namespace HRInPocket.Extensions
{
    public static class ProfileExtention
    {
        public static UserProfileViewModel ToViewModel(this Profile profile) =>
            new UserProfileViewModel
            {
                Surname = profile.Surname,
                FirstName = profile.FirstName,
                Patronymic = profile.Patronymic,
                Age = profile.Birthday == default ? (int?) null : GetFullYears(profile.Birthday, DateTime.Now),
                Birthday = profile.Birthday,
                Sex = profile.Sex,
                Address = profile.Address,
                Specialities = profile.Speciality.ToList(),
                Resumes = profile.Resumes.ToList(),
                CoverLetters = profile.CoverLetters.ToList()
            };


        private static int GetFullYears(DateTime dt1, DateTime dt2)
        {
            if (dt2.Year <= dt1.Year)
                return 0;
            var n = dt2.Year - dt1.Year;
            if (dt1.DayOfYear >= dt2.DayOfYear)
                --n;
            return n;
        }
    }
}
