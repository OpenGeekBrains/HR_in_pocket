using System.Threading.Tasks;
using HRInPocket.IdentityServer.Controllers.Helpers;
using HRInPocket.IdentityServer.Models;
using HRInPocket.IdentityServer.ViewModels;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.IdentityServer.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IIdentityServerInteractionService _Interaction;
        private readonly IClientStore _ClientStore;
        private readonly IAuthenticationSchemeProvider _SchemeProvider;
        private readonly IEventService _Events;

        public RegisterController(UserManager<ApplicationUser> UserManager,
            SignInManager<ApplicationUser> SignInManager,
            IIdentityServerInteractionService interaction,
            IClientStore ClientStore,
            IAuthenticationSchemeProvider SchemeProvider,
            IEventService events)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Interaction = interaction;
            _ClientStore = ClientStore;
            _SchemeProvider = SchemeProvider;
            _Events = events;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl ) => View(new RegisterViewModel{ReturnUrl = ReturnUrl});

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _SignInManager.SignInAsync(user, false);
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
