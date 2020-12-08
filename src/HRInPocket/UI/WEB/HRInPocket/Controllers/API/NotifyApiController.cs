using System.Linq;

using HRInPocket.Infrastructure.Models.JsonReturnModels;
using HRInPocket.Infrastructure.Models.Records;
using HRInPocket.Infrastructure.Services;
using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Notify)]
    [ApiController]
    public class NotifyApiController : ControllerBase
    {
        private readonly NotifyService _notifyService;
        private readonly ILogger<NotifyApiController> _logger;

        public NotifyApiController(NotifyService notifyService, ILogger<NotifyApiController> logger)
        {
            _notifyService = notifyService;
            _logger = logger;
        }
        
        [HttpGet]
        public JsonResult GetNotifies() => new(new ArrayContent(_notifyService.GetNotifyUsers(), _notifyService.GetNotifyUsers().Any()));
        
        [HttpPost("/auth")]
        public JsonResult CheckAuthEmail([FromQuery] string key, [FromQuery] string token, [FromQuery] string email)
        {
            _notifyService.NotifyMe(new NotifyUser(email){EmailNotify = true});
            return new JsonResult(new { key, token, email });
        }

        [HttpGet("/unsubscribe")]
        public IActionResult UnsubscribeMailSend(string email) => _notifyService.UnsubscribeEmail(email) ? BadRequest() : Ok();
    }
}
