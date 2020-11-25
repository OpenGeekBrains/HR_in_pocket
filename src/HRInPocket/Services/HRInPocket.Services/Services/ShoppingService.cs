using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.DAL.Models.Base;
using HRInPocket.DAL.Models.Entities;
using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Services
{
    public class ShoppingService : IShoppingService
    {
        /// <summary>
        /// Провайдер данных </summary>
        private readonly IDataRepository _DataProvider;

        private readonly IMapper<Tarif, TarifDTO> _TarifMapper;
        //private readonly IMapper<Service, ServiceDTO> _ServiceMapper;

        public ShoppingService(
            IDataRepository dataProvider,
            IMapper<Tarif, TarifDTO> tarifMapper
            //,IMapper<Service, ServiceDTO> serviceMapper
            )
        {
            _DataProvider = dataProvider;
            _TarifMapper = tarifMapper;
            //_ServiceMapper = serviceMapper;
        }

        public Task<bool> ArchivingTariffPlanAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TarifDTO> ChoiceTariffPlanAsync(long id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateTariffPlanAsync(TarifDTO tarif)
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

        public Task<ServiceDTO> GetServiceById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceDTO>> GetServiceHistoryAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<TarifDTO> GetTariffPlanByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TarifDTO>> GetTariffPlansAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrderServiceAsync(long serviceId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveTariffPlanAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
