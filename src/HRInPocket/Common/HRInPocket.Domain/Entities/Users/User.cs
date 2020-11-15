using System.ComponentModel.DataAnnotations;
using HRInPocket.DAL.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HRInPocket.DAL.Models.Users
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Профиль пользователя
        /// </summary>
        [Required]
        public Profile Profile { get; set; }
        public string ProfileId { get; set; }
    }
}
