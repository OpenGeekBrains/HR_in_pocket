using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace HRInPocket.DAL.Models.Users
{
    public class User : IdentityUser
    {
        [Required]
        public Profile Profile { get; set; }
    }
}
