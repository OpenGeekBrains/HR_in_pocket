using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Mapper
{
    public class ResumeMapper : BaseMapper<Resume, ResumeDTO>, IMapper<Resume, ResumeDTO>
    {
        public ResumeDTO Map(Resume entity) => ToDTO(entity,
            cfg => cfg.CreateMap<Resume, ResumeDTO>());

        public Resume Map(ResumeDTO entity) => FromDTO(entity,
            cfg => cfg.CreateMap<ResumeDTO, Resume>());
    }
}
