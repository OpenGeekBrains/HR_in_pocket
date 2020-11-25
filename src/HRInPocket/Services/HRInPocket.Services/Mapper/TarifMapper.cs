using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Mapper
{
    public class TarifMapper : BaseMapper<Tarif, TarifDTO>, IMapper<Tarif, TarifDTO>
    {
        public TarifDTO Map(Tarif entity) => ToDTO(entity,
            cfg => cfg.CreateMap<Tarif, TarifDTO>());

        public Tarif Map(TarifDTO entity) => FromDTO(entity,
            cfg => cfg.CreateMap<TarifDTO, Tarif>());
    }
}
