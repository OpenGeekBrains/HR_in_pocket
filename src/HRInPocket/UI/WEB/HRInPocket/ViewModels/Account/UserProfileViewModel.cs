using System;
using System.Collections.Generic;
using HRInPocket.Domain;
using HRInPocket.Domain.Entities.Data;

namespace HRInPocket.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }

        /// TODO: заменить Address на ViewModel
        public Address Address { get; set; }
        public List<Resume> Resumes { get; set; }
        public List<CoverLetter> CoverLetters { get; set; }
        public List<Speciality> Specialities { get; set; }
    }
}
