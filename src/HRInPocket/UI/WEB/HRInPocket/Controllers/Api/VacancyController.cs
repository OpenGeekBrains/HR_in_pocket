using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
using HRInPocket.Domain.Models.Resume;
using HRInPocket.Interfaces.Services;

//using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.Api
{
    [ApiController]
    [Route("api/Vacancy")]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _VacancyService;

        public VacancyController(IVacancyService vacancyService) => _VacancyService = vacancyService;

        /// <summary>
        /// Получения списка вакансий
        /// </summary>
        //[EnableQuery()]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacancyDTO>>> Get() => (await _VacancyService.GetVacanciesAsync()).Vacancies.ToList();

        /// <summary>
        /// Просмотр вакансий компании
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("ByCompany/{id}")]
        public async Task<PageVacancyDTO> GetCompanyVacancies(Guid id) => await _VacancyService.GetCompanyVacanciesAsync(id);

        /// <summary>
        /// Создать вакансию
        /// </summary>
        /// <param name="vacancy"></param>
        [HttpPost]
        public async Task<Guid> Create([FromBody] VacancyDTO vacancy) => await _VacancyService.CreateVacancyAsync(vacancy);

        /// <summary>
        /// Редактировать вакансию
        /// </summary>
        /// <param name="vacancy"></param>
        [HttpPut]
        public async Task<bool> Edit([FromBody] VacancyDTO vacancy) => await _VacancyService.EditVacancyAsync(vacancy);
        
        /// <summary>
        /// Удалить вакансию
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<bool> Remove(Guid id) => await _VacancyService.RemoveVacancyAsync(id);
    }
}
