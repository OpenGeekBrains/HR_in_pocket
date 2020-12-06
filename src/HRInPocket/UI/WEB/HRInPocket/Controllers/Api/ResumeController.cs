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
    [Route("api/Resume")]
    public class ResumeController : Controller
    {
        private readonly IResumeService _ResumeService;

        public ResumeController(IResumeService resumeService) => _ResumeService = resumeService;

        /// <summary>
        /// Посмотреть весь список резюме
        /// </summary>
        /// <param name="filter"></param>
        [HttpGet]
        public async Task<IEnumerable<ResumeDTO>> Get() => (await _ResumeService.GetResumesAsync()).Items.ToList();

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
        public async Task<ResumeDTO> GetResumeByIdAsync(Guid id) =>await _ResumeService.GetResumeByIdAsync(id);

        /// <summary>
        /// Создать резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        [HttpPost]
        public async Task<Guid> CreateResumeAsync([FromBody]  ResumeDTO resume) => await _ResumeService.CreateResumeAsync(resume);

        /// <summary>
        /// Редактировать информацию в резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        [HttpPut]
        public async Task<bool> EditResumeAsync([FromBody] ResumeDTO resume) => await _ResumeService.EditResumeAsync(resume);

        /// <summary>
        /// Удалить резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        [HttpDelete("{id}")]
        public async  Task<bool> RemoveResumeAsync(Guid id) => await _ResumeService.RemoveResumeAsync(id);
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
    }
}
