using System.Threading.Tasks;
using HRInPocket.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers
{
    [Authorize] // если пользователь не зарегистрирован - будет редирект на сервер для входа,
                // после чего вернятся обратно на запрашиваемую страницу
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _Logger;

        public AccountController(ILogger<AccountController> Logger) => _Logger = Logger;

        #region Register

        //[HttpGet]
        //public IActionResult Register() => View();
        public IActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var user = new ApplicationUser { Email = model.Email, UserName = model.Name };
        //    // добавляем пользователя
        //    var result = await _UserManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        // установка куки
        //        await _SignInManager.SignInAsync(user, false);
        //        return RedirectToAction("Index", "Home");
        //    }

        //    foreach (var error in result.Errors)
        //        ModelState.AddModelError(string.Empty, error.Description);

        //    return View(model);
        //}

        #endregion

        #region Login

        //[HttpGet]
        //public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });
        public IActionResult Login(string ReturnUrl) => RedirectToAction("Index", "Home");

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    //var login_result = await _SignInManager.PasswordSignInAsync(
        //    //    model.UserName,
        //    //    model.Password,
        //    //    model.RememberMe,
        //    //    lockoutOnFailure: false);

        //    //if (login_result.Succeeded)
        //    //{
        //    //    if (Url.IsLocalUrl(model.ReturnUrl))
        //    //        return Redirect(model.ReturnUrl);
        //    //    return RedirectToAction("Index", "Home");
        //    //}

        //    ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

        //    return View(model);
        //}

        #endregion

        //await _SignInManager.SignOutAsync();
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }


        public async Task<IActionResult> Profile([FromServices] ITasksService TasksService)
        {
            //var profile = _UserManager.GetUserAsync(User).Result.Profile ?? new Profile();
            //var view_model = profile.ToViewModel();

            //var user_id = _UserManager.GetUserId(User);

            //view_model.Tasks = await TasksService.GetUserTasks(user_id);

            return View(/*view_model*/);
        }

        //public string UserId() => _UserManager.GetUserId(User);
    }
}