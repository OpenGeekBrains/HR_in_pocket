using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HRInPocket.ViewModels;
using HRInPocket.ViewModels.MakeTask;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var context = HttpContext;
            //string str = string.Empty;
            //foreach (var user_claim in context.User.Claims)
            //{
            //    str += user_claim.Type + " => " + user_claim.Value + ";\n";
            //}
            //ViewBag.All = str;

            //string name = context.User.FindFirst(c => c.Type.Equals("name")).Value;
            //string email = context.User.FindFirst(c => c.Type.Equals("email")).Value;

            //ViewBag.Name = name;
            //ViewBag.Email = email;

            return View(new ShortFormCreateTaskViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
