using System.Linq;

using HRInPocket.Domain.Models.JsonReturnModels;
using HRInPocket.Interfaces;
using HRInPocket.Services.Services;

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

        public FeedBackApiController(ILogger<FeedBackApiController> Logger, FeedBackService feedBackService)
        {
            _Logger = Logger;
            _feedbackService = feedBackService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var content = _feedbackService.Requests.GroupBy(f => f.email).ToArray();
            return new JsonResult(new ArrayContent(content, content.Any()));
        }

        [HttpPost("/user/feedback")]
        public IActionResult TakeFeedBack(string name, string email, string phone_number, string message)
        {
            return Ok();
        }
    }
}
