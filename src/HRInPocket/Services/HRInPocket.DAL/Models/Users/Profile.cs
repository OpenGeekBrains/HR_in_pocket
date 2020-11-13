using System.Collections.Generic;
using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    public class Profile : BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<string> Emails { get; set; }
        public ICollection<string> Phones { get; set; }
    }
}