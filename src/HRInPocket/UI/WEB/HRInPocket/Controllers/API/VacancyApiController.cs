﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;

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
        private readonly IVacancyService _VacancyService;

        public VacancyApiController(IVacancyService vacancyService,/* Сервис/менеджер вакансий ,*/ ILogger<VacancyApiController> Logger)
        {
            _Logger = Logger;
            _VacancyService = vacancyService;
        }

        //todo: CRUD-интерфейс к вакансиям с постраничным разбиением
        
        /// <summary>
        /// Получения списка вакансий
        /// </summary>
        //[EnableQuery()]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacancyDTO>>> Get() => (await _VacancyService.GetAllAsync()).Entities.ToList();

        /// <summary>
        /// Просмотр вакансий компании
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("ByCompany/{id}")]
        public async Task<PageDTOs<VacancyDTO>> GetCompanyVacancies(Guid id) => await _VacancyService.GetCompanyVacanciesAsync(id);

        /// <summary>
        /// Создать вакансию
        /// </summary>
        /// <param name="vacancy"></param>
        [HttpPost]
        public async Task<Guid> Create([FromBody] VacancyDTO vacancy) => await _VacancyService.CreateAsync(vacancy);

        /// <summary>
        /// Редактировать вакансию
        /// </summary>
        /// <param name="vacancy"></param>
        [HttpPut]
        public async Task<bool> Edit([FromBody] VacancyDTO vacancy) => await _VacancyService.EditAsync(vacancy);
        
        /// <summary>
        /// Удалить вакансию
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<bool> Remove(Guid id) => await _VacancyService.RemoveAsync(id);
    }
}
