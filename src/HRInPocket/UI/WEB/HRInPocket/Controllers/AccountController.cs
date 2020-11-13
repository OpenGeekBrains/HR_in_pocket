using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult Profile() => View();
    }
}