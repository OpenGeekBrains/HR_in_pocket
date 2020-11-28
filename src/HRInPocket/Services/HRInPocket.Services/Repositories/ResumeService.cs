using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Domain.Models.Resume;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services.Repository;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Repositories
{
    public class ResumeService : IResumeService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IDataRepository<Resume> _DataProvider;
        private readonly IMapper _Mapper;

        public ResumeService(IDataRepository<Resume> dataProvider, IMapper mapper)
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }


        #region IRepository implementation

        /// <summary>
        /// Посомтреть весь список резюме
        /// </summary>
        public async Task<PageDTOs<ResumeDTO>> GetAllAsync(Filter filter)
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

            return new PageDTOs<ResumeDTO>
            {
                Entities = query.Select(q => _Mapper.Map<ResumeDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посомтреть информацию о резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        public async Task<ResumeDTO> GetByIdAsync(Guid id) =>
            _Mapper.Map<ResumeDTO>((await _DataProvider.GetByIdAsync(id)));

        /// <summary>
        /// Создать резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        public async Task<Guid> CreateAsync(ResumeDTO resume) =>
            await _DataProvider.CreateAsync(_Mapper.Map<Resume>(resume));

        /// <summary>
        /// Редактировать информацию в резюме
        /// </summary>
        /// <param name="resume">Модель представления резюме</param>
        public async Task<bool> EditAsync(ResumeDTO resume) =>
            await _DataProvider.EditAsync(_Mapper.Map<Resume>(resume));

        /// <summary>
        /// Удалить резюме по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор резюме</param>
        public async Task<bool> RemoveAsync(Guid id) =>
            await _DataProvider.RemoveAsync(id); 

        #endregion


        /// <summary>
        /// Посмотреть список резюме пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public async Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id) =>
            (await _DataProvider.GetQueryableAsync())
            .Where(r => r.Applicant.ProfileId == id.ToString())
            .Select(r => _Mapper.Map<ResumeDTO>(r));


        /// <summary>
        /// Загрузить файл резюме
        /// </summary>
        /// <param name="resumeFile">Модель файла резюме</param>
        public async Task<bool> UploadResumeFileAsync(ResumeFile resumeFile)
        {
            throw new NotImplementedException();
        }

        // Методы поиска резюме

        public async Task SearchResumesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
