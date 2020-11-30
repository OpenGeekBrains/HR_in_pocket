using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Models.Resume;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services.Repository;

namespace HRInPocket.Services.Repositories
{
    public class ResumeService : DtoRepository<Resume, ResumeDTO>, IResumeService
    {
        public ResumeService(IDataRepository<Resume> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id) =>
            (await _DataProvider.GetQueryableAsync())
            .Where(r => r.Applicant.ProfileId == id.ToString())
            .AsEnumerable()
            .Select(_Mapper.Map<ResumeDTO>);


        /// <inheritdoc/>
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
