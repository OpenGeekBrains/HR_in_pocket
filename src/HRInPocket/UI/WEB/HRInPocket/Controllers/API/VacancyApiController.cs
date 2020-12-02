using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Vacancy)]
    [ApiController]
    //[Authorize(Roles = "VacancyManager,Administrator")]
    public class VacancyApiController : ControllerBase
    {
        private readonly ILogger<VacancyApiController> _Logger;

        public VacancyApiController(/* Сервис/менеджер вакансий ,*/ ILogger<VacancyApiController> Logger)
        {
            _Logger = Logger;
        }

        //todo: CRUD-интерфейс к вакансиям с постраничным разбиением
    }
}
