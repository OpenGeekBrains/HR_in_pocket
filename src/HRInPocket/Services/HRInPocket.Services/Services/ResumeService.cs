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

        public Task<Guid> CreateResumeAsync(ResumeDTO resume)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditResumeAsync(ResumeDTO resume)
        {
            throw new NotImplementedException();
        }

        public Task<ResumeDTO> GetResumeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PageResumeDTO> GetResumesAsync(ResumeFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResumeDTO>> GetUserResumesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveResumeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SearchResumesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadResumeFileAsync(ResumeFile resumeFile)
        {
            throw new NotImplementedException();
        }
    }
}
