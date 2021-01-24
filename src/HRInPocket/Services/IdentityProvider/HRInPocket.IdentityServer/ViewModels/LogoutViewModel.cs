
using HRInPocket.IdentityServer.Models;

namespace HRInPocket.IdentityServer.ViewModels
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; }
    }
}
