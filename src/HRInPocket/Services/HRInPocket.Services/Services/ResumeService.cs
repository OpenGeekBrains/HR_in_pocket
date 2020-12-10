using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Models.Resume;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces.Services;
using HRInPocket.Interfaces;
using AutoMapper;
using HRInPocket.Domain.Entities.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HRInPocket.Services.Services
{
    public class ResumeService : IResumeService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository<Resume> _DataProvider;
        private readonly IMapper _Mapper;

        public ResumeService(IDataRepository<Resume> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        /// <summary>
        /// Посомотреть весь список резюме
        /// </summary>
        public async Task<PageResumeDTO> GetResumesAsync(ResumeFilter filter)
        {
            var query = _DataProvider.GetQueryable();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/
            }

            var count = await query.CountAsync();

            query = query
                .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                .Take(filter.Pages.PageSize);

            return new PageResumeDTO
            {
                Items = query.Select(q => _Mapper.Map<ResumeDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть список резюме пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public async Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id) =>
            (await _DataProvider.GetQueryableAsync())
            .Where(r => r.Applicant.ProfileId == id.ToString())
            .Select(r => _Mapper.Map<ResumeDTO>(r));

        /// <summary>
        /// Посмотреть информацию о резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        public async Task<ResumeDTO> GetResumeByIdAsync(Guid id) => 
            _Mapper.Map<ResumeDTO>((await _DataProvider.GetByIdAsync(id)));

        /// <summary>
        /// Создать резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        public async Task<Guid> CreateResumeAsync(ResumeDTO resume) =>
            await _DataProvider.CreateAsync(_Mapper.Map<Resume>(resume));

        /// <summary>
        /// Редактировать информацию в резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        public async Task<bool> EditResumeAsync(ResumeDTO resume) =>
            await _DataProvider.EditAsync(_Mapper.Map<Resume>(resume));

        /// <summary>
        /// Удалить резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        public async Task<bool> RemoveResumeAsync(Guid id) => 
            await _DataProvider.RemoveAsync(id);

        /// <summary>
        /// Загрузить файл резюме
        /// </summary>
        /// <param name="resumeFile">Модель файла резюме</param>
        public Task<bool> UploadResumeFileAsync(ResumeFile resumeFile) => throw new NotImplementedException();

        // Методы поиска резюме

        public Task SearchResumesAsync() => throw new NotImplementedException();

    }
}
