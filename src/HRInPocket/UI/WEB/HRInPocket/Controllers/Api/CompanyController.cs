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
        public async Task<IEnumerable<CompanyDTO>> Get() => (await _CompanyService.GetCompanies()).Items.ToList();

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
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyDTO company) =>
            CreatedAtAction("Get", await _CompanyService.CreateCompanyAsync(company));
        /// <summary>
        /// Редактирвание информации о компании
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EditCompanyAsync([FromBody] CompanyDTO company) => await _CompanyService.EditCompanyAsync(company);

        /// <summary>
        /// Удаление компании по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<bool> RemoveCompanyAsync(Guid id) => await _CompanyService.RemoveCompanyAsync(id);
    }
}
