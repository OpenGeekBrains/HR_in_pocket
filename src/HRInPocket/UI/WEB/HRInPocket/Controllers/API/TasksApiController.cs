using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Tasks)]
    [ApiController]
    public class TasksApiController : ControllerBase
    {
        private readonly ITasksService _TasksService;
        private readonly ILogger<TasksApiController> _Logger;

        public TasksApiController(ITasksService TasksService, ILogger<TasksApiController> Logger)
        {
            _TasksService = TasksService;
            _Logger = Logger;
        }

        [HttpGet("{UserId}")]
        [HttpGet("{UserId}({Index},{Count})")]
        public async Task<ActionResult<IEnumerable<TargetTask>>> Get(string UserId, int Index = 0, int Count = -1)
        {
            if (UserId is null)
                return BadRequest();

            var tasks = await _TasksService.GetUserTasks(UserId);
            return new ActionResult<IEnumerable<TargetTask>>(tasks);
        }

        //todo: Добавление задачи

        //todo: Редактирование задачи

        //todo: Включение/выключение

        //todo: Удаление (удаляемая задача не должна удаляться из БД! Запись в БД должна помечаться как удалённая и исключаться из выдачи)
    }
}
