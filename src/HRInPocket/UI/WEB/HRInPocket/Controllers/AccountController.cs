using System;
using HRInPocket.DAL.Models;
using HRInPocket.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult Profile() => View(new UserProfileViewModel
        {
            FirstName = "Somebody",
            Surname = "Something",
            Patronymic = "From Somebody",
            Age = 47,
            Birthday = DateTime.Now.AddYears(-47),
            Sex = Sex.Other
        });
    }
}