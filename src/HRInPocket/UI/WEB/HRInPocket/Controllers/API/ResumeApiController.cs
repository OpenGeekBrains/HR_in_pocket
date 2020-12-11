using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.Resume)]
    [ApiController]
    public class ResumeApiController : ControllerBase
    {
        private readonly ILogger<ResumeApiController> _Logger;
        private readonly IResumeService _ResumeService;

        public ResumeApiController(IResumeService resumeService,/* Менеджер/сервис управления резюме ,*/ ILogger<ResumeApiController> Logger)
        {
            _Logger = Logger;
            _ResumeService = resumeService;
        }

        // todo: CRUD-интерфейс для работы с резюме
        
        /// <summary>
        /// Посмотреть весь список резюме
        /// </summary>
        /// <param name="filter"></param>
        [HttpGet]
        public async Task<IEnumerable<ResumeDTO>> Get() => (await _ResumeService.GetAllAsync()).Entities.ToList();

        /// <summary>
        ///  Посмотреть список резюме пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        [HttpGet("User/{id}")]
        public async Task<IEnumerable<ResumeDTO>> GetUser(Guid id) => (await _ResumeService.GetUserResumesAsync(id)).ToList();

        /// <summary>
        /// Посомотреть информацию о резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ResumeDTO> GetResumeByIdAsync(Guid id) =>await _ResumeService.GetByIdAsync(id);

        /// <summary>
        /// Создать резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        [HttpPost]
        public async Task<Guid> CreateResumeAsync([FromBody]  ResumeDTO resume) => await _ResumeService.CreateAsync(resume);

        /// <summary>
        /// Редактировать информацию в резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        [HttpPut]
        public async Task<bool> EditResumeAsync([FromBody] ResumeDTO resume) => await _ResumeService.EditAsync(resume);

        /// <summary>
        /// Удалить резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        [HttpDelete("{id}")]
        public async  Task<bool> RemoveResumeAsync(Guid id) => await _ResumeService.RemoveAsync(id);
/*
        /// <summary>
        /// Поиск резюме
        /// </summary>
        public  Task SearchResumesAsync() => throw new NotImplementedException();

        /// <summary>
        /// Загрузка файла резюме
        /// </summary>
        /// <param name="resumeFile">Модель файла резюме</param>
        /// <returns></returns>
        public Task<bool> UploadResumeFileAsync(ResumeFile resumeFile) => throw new NotImplementedException();*/

        // todo: метод получения файла от клиента с его резюме
    }
}
