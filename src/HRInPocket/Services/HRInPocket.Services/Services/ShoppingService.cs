using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Services
{
    public class ShoppingService : IShoppingService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository<Tarif> _DataProvider;
        private readonly IMapper _Mapper;

        public ShoppingService(
            IDataRepository<Tarif> dataProvider,
            IMapper mapper
            )
        {
            _DataProvider = dataProvider;
            _Mapper = mapper;
        }

        public Task<bool> ArchivingTariffPlanAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TarifDTO> ChoiceTariffPlanAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateTariffPlanAsync(TarifDTO tarif)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditTariffPlanAsync(TarifDTO tarif)
        {
            throw new NotImplementedException();
        }

        public Task<PageServiceDTO> GetAllServicesAsync(ServiceFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceDTO>> GetAllServicesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceDTO>> GetOpenServicesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDTO> GetServiceById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceDTO>> GetServiceHistoryAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<TarifDTO> GetTariffPlanByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TarifDTO>> GetTariffPlansAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrderServiceAsync(Guid serviceId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveTariffPlanAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
