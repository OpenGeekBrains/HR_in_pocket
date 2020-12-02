using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.TasksManager)]
    [ApiController]
    public class TaskManagerApiController : ControllerBase
    {
        private readonly ITaskManager _TaskManager;
        private readonly ILogger<TaskManagerApiController> _Logger;

        public TaskManagerApiController(ITaskManager TaskManager, ILogger<TaskManagerApiController> Logger)
        {
            _TaskManager = TaskManager;
            _Logger = Logger;
        }

        //todo: Запустить процесс выполнения задач
        //todo: Остановить процесс выполнения задач
        //todo: Определить статус процесса (запущен/остановлен/ошибка)
        //todo: Список ошибок
        //todo: Читать/изменять значение таймаута
    }
}
