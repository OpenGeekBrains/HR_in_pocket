using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;
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
        private readonly ITargetTaskService _TargetTaskService;

        public TasksApiController(ITargetTaskService targetTaskService, ITasksService TasksService, ILogger<TasksApiController> Logger)
        {
            _TasksService = TasksService;
            _Logger = Logger;
            _TargetTaskService = targetTaskService;
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

        
        //[EnableQuery()]
        [HttpGet]
        public async Task<IEnumerable<TargetTaskDTO>> Get() => (await _TargetTaskService.GetAllAsync()).Entities.ToList();
        [HttpGet("{id}")]
        public async Task<TargetTaskDTO> GetTargetTaskByIdAsync(Guid id) => await _TargetTaskService.GetByIdAsync(id);
        [HttpGet("ByUser/{id}")]
        public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => await _TargetTaskService.GetTargetTasksByUserAsync(id);
        [HttpPost]
        public async Task<Guid> CreateTargetTaskAsync([FromBody] TargetTaskDTO task) => await _TargetTaskService.CreateAsync(task);
        [HttpPut]
        public async Task<bool> EditTargetTaskAsync([FromBody] TargetTaskDTO task) => await _TargetTaskService.EditAsync(task);
        [HttpDelete("{id}")]
        public async Task<bool> RemoveTargetTaskAsync(Guid id) => await _TargetTaskService.RemoveAsync(id);
        [HttpGet("Execute/{id}")]
        public async Task<bool> ExecuteTargetTaskAsync(Guid id) => await _TargetTaskService.ExecuteTargetTaskAsync(id);
        [HttpGet("Abort/{id}")]
        public async Task<bool> AbortTargetTaskAsync(Guid id) => await _TargetTaskService.AbortTargetTaskAsync(id);
        
        
        //todo: Добавление задачи

        //todo: Редактирование задачи

        //todo: Включение/выключение

        //todo: Удаление (удаляемая задача не должна удаляться из БД! Запись в БД должна помечаться как удалённая и исключаться из выдачи)
    }
}
