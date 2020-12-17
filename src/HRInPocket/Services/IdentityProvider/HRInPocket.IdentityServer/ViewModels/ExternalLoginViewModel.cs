using System.ComponentModel.DataAnnotations;

namespace HRInPocket.IdentityServer.ViewModels
{

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
