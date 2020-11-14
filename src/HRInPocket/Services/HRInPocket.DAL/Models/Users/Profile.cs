using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;

namespace HRInPocket.DAL.Models.Users
{
    public class Profile : BaseEntity
    {
        [Required]
        public User User { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<string> Emails { get; set; }
        public ICollection<string> Phones { get; set; }
    }
}