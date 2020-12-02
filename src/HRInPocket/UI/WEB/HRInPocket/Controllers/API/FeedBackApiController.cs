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
    }
}
