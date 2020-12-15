using System.ComponentModel.DataAnnotations;

namespace HRInPocket.IdentityServer.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}