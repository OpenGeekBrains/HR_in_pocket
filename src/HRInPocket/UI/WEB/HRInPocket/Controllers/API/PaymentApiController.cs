using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Payment)]
    [ApiController]
    public class PaymentApiController : ControllerBase
    {
        private readonly ILogger<PaymentApiController> _Logger;

        public PaymentApiController(/* Менеджер/сервис управления оплатой ,*/ ILogger<PaymentApiController> Logger)
        {
            _Logger = Logger;
        }
    }
}
