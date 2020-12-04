using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces.Services;

using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.Api
{
    [ApiController]
    [Route("api/vacancy")]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _VacanceyService;

        public VacancyController(IVacancyService vacanceyService)
        {
            _VacanceyService = vacanceyService;
        }

        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<IEnumerable<VacancyDTO>>> Get() =>
            (await _VacanceyService.GetVacanciesAsync()).Vacancies.ToList();

        [HttpGet("ByCompany/{id}")]
        public async Task<PageVacancyDTO> GetCompanyVacancies(Guid id) => await _VacanceyService.GetCompanyVacanciesAsync(id);

        [HttpPost]
        public async Task<Guid> Create([FromBody] VacancyDTO vacancy) => await _VacanceyService.CreateVacancyAsync(vacancy);
        
        [HttpPut("{id}")]
        public async Task<bool> Edit([FromBody] VacancyDTO vacancy) => await _VacanceyService.EditVacancyAsync(vacancy);
        
        [HttpDelete("{id}")]
        public async Task<bool> Remove(Guid id) => await _VacanceyService.RemoveVacancyAsync(id);
    }
}
