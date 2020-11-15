using System;
using System.Threading.Tasks;
using HRInPocket.Domain.Entities.Users;
using HRInPocket.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> user_manager, SignInManager<User> sign_in_manager)
        {
            _UserManager = user_manager;
            _SignInManager = sign_in_manager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Name };
                // добавляем пользователя
                var result = await _UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
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

        // GET
        public IActionResult Profile() => View(new UserProfileViewModel
        {
            FirstName = "Somebody",
            Surname = "Something",
            Patronymic = "From Somebody",
            Age = 47,
            Birthday = DateTime.Now.AddYears(-47),
            //Sex = Sex.Other
        });
    }
}