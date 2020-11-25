using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Mapper
{
    public class TargetTaskMapper : BaseMapper<TargetTask, TargetTaskDTO>, IMapper<TargetTask, TargetTaskDTO>
    {
        public TargetTaskDTO Map(TargetTask entity) => ToDTO(entity,
            cfg => cfg.CreateMap<TargetTask, TargetTaskDTO>());

        public TargetTask Map(TargetTaskDTO entity) => FromDTO(entity,
            cfg => cfg.CreateMap<TargetTaskDTO, TargetTask>());
    }
}
