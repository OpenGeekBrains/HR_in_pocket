using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers
{
    [ApiController]
    [Route("api/testapi")]
    public class TestApiController : Controller
    {
        private static readonly IEnumerable<string> Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public TestApiController()
        {
            
        }

        [HttpGet("data")]
        public IEnumerable<string> GetAllData() => Summaries;
    }
}
