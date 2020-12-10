using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;
using HRInPocket.Interfaces.Repository.Base;
using HRInPocket.Services.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories
{
    public class VacancyService : DtoRepository<Vacancy, VacancyDTO>, IVacancyService
    {
        public VacancyService(IDataRepository<Vacancy> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
        }

        /// <inheritdoc/>
        public async Task<PageDTOs<VacancyDTO>> GetCompanyVacanciesAsync(Guid id)
        {
            var query = (await _DataProvider.GetQueryableAsync())
                .Where(v => v.Company.Id == id);

            var count = await query.CountAsync();

            return new PageDTOs<VacancyDTO>
            {
                Entities = query.AsEnumerable().Select(_Mapper.Map<VacancyDTO>),
                TotalCount = count,
            };
        }

        // Методы поиска вакансий (через API, из тех которые были сформированы по результатам парсинга сайтов)

        /// <inheritdoc/>
        public Task SearchVacanciesAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}