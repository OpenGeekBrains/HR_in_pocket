using System;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Entities.Users;
using HRInPocket.Extensions;
using HRInPocket.Interfaces.Services;
using HRInPocket.ViewModels.Account;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User { Email = model.Email, UserName = model.Name };
            // добавляем пользователя
            var result = await _UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // установка куки
                await _SignInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (login_result.Succeeded)
            {
                if (Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

            return View(model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        
        public async Task<IActionResult> Profile([FromServices] ITasksService TasksService)
        {
            var profile = _UserManager.GetUserAsync(User).Result.Profile ?? new Profile();
            var view_model = profile.ToViewModel();

            var user_id = _UserManager.GetUserId(User);

            view_model.Tasks = await TasksService.GetUserTasks(user_id);

            return View(view_model);
        }

        public string UserId() => _UserManager.GetUserId(User);
    }
}