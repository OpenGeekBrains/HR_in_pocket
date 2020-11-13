using System.Collections.Generic;

using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        /// TODO: заменить Address на ViewModel
        public List<Address> Addresses { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Phones { get; set; }
    }
}
