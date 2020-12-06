﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
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

    [ApiController]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _CompanyService;

        public CompanyController(ICompanyService companyService) => _CompanyService = companyService;

        //[EnableQuery()]
        /// <summary>
        /// Посмотреть информацию о всех компаниях
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageCompanyDTO> Get() => await _CompanyService.GetCompanies();

        /// <summary>
        /// Посмотреть информацию о компании по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CompanyDTO> GetCompanyById(Guid id) => await _CompanyService.GetCompanyById(id);

        /// <summary>
        ///  Создать компанию
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> CreateCompanyAsync(CompanyDTO company) => await _CompanyService.CreateCompanyAsync(company);
        /// <summary>
        /// Редактирвание информации о компании
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EditCompanyAsync(CompanyDTO company) => await _CompanyService.EditCompanyAsync(company);

        /// <summary>
        /// Удаление компании по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> RemoveCompanyAsync(Guid id) => await _CompanyService.RemoveCompanyAsync(id);
    }
}
