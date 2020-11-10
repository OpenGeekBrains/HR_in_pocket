using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index() => View();
    }
}
