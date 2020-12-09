using System.Linq;

using HRInPocket.Domain.Models.JsonReturnModels;
using HRInPocket.Infrastructure.Models.Records;
using HRInPocket.Interfaces;
using HRInPocket.Services.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Notify)]
    [ApiController]
    public class NotifyApiController : ControllerBase
    {
        private readonly ILogger<NotifyApiController> _logger;
        private readonly NotifyService _notifyService;

        public NotifyApiController(ILogger<NotifyApiController> logger)
        {
            _logger = logger;
            _notifyService = new NotifyService();
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
