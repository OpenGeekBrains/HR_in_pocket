using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    public class MakeTaskController : Controller
    {
        public IActionResult Index() => View();
    }
}
