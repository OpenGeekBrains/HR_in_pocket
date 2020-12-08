using System.Linq;

using HRInPocket.Infrastructure.Services;
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
        private readonly FeedBackService _feedbackService;

        public FeedBackApiController(ILogger<FeedBackApiController> Logger)
        {
            _Logger = Logger;
            _feedbackService = new FeedBackService();
        }

        [HttpGet]
        public JsonResult Get() => new JsonResult(_feedbackService.Requests.GroupBy(f=>f.email));
        
        [HttpPost("/user/feedback")]
        public IActionResult TakeFeedBack(string name, string email, string phone_number, string message)
        {
            return Ok();
        }
    }
}
