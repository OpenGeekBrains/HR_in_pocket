using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.FeedBack)]
    [ApiController]
    public class FeedBackApiController : ControllerBase
    {
        private readonly ILogger<FeedBackApiController> _Logger;

        public FeedBackApiController(ILogger<FeedBackApiController> Logger)
        {
            _Logger = Logger;
        }

        [HttpPost("/user/feedback")]
        public IActionResult TakeFeedBack(string name, string email, string phone_number, string message)
        {
            return Ok();
        }
    }
}
