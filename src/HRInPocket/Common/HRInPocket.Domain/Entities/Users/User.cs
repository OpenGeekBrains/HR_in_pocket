using System.ComponentModel.DataAnnotations;
using HRInPocket.Domain.Entities.Data;

using Microsoft.AspNetCore.Identity;

namespace HRInPocket.Domain.Entities.Users
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
