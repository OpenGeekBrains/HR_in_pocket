using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Interfaces.Services;

//using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.Api
{
    [ApiController]
    [Route("api/TargetTask")]
    public class TargetTaskController : Controller
    {
        private readonly ITargetTaskService _TargetTaskService;

        public TargetTaskController(ITargetTaskService targetTaskService) => _TargetTaskService = targetTaskService;

        //[EnableQuery()]
        [HttpGet]
        public async Task<IEnumerable<TargetTaskDTO>> Get() => (await _TargetTaskService.GetAllTargetTasksAsync()).TargetTasks.ToList();
        [HttpGet("{id}")]
        public async Task<TargetTaskDTO> GetTargetTaskByIdAsync(Guid id) => await _TargetTaskService.GetTargetTaskByIdAsync(id);
        [HttpGet("ByUser/{id}")]
        public async Task<IEnumerable<TargetTaskDTO>> GetTargetTasksByUserAsync(Guid id) => await _TargetTaskService.GetTargetTasksByUserAsync(id);
        [HttpPost]
        public async Task<Guid> CreateTargetTaskAsync([FromBody] TargetTaskDTO task) => await _TargetTaskService.CreateTargetTaskAsync(task);
        [HttpPut]
        public async Task<bool> EditTargetTaskAsync([FromBody] TargetTaskDTO task) => await _TargetTaskService.EditTargetTaskAsync(task);
        [HttpDelete("{id}")]
        public async Task<bool> RemoveTargetTaskAsync(Guid id) => await _TargetTaskService.RemoveTargetTaskAsync(id);
        [HttpGet("Execute/{id}")]
        public async Task<bool> ExecuteTargetTaskAsync(Guid id) => await _TargetTaskService.ExecuteTargetTaskAsync(id);
        [HttpGet("Abort/{id}")]
        public async Task<bool> AbortTargetTaskAsync(Guid id) => await _TargetTaskService.AbortTargetTaskAsync(id);
    }
}
