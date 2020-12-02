using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Resume)]
    [ApiController]
    public class ResumeApiController : ControllerBase
    {
        private readonly ILogger<ResumeApiController> _Logger;

        public ResumeApiController(/* Менеджер/сервис управления резюме ,*/ ILogger<ResumeApiController> Logger)
        {
            _Logger = Logger;
        }

        // todo: CRUD-интерфейс для работы с резюме

        // todo: метод получения файла от клиента с его резюме
    }
}
