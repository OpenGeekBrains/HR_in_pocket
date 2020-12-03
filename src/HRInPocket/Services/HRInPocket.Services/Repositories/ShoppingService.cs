using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Repository;
using HRInPocket.Interfaces.Repository.Base;

namespace HRInPocket.Services.Repositories
{
    public class ShoppingService : IShoppingService
    {
        /// <summary>
        /// Провайдер данных
        /// </summary>
        private readonly IDataRepository<Tarif> _TarifDataProvider;
        private readonly IDataRepository<PriceItem> _PriceItemDataProvider;
        private readonly IMapper _Mapper;

        public ShoppingService(
            IDataRepository<Tarif> tarifDataProvider,
            IDataRepository<PriceItem> priceItemDataProvider,
            IMapper mapper
            )
        {
            _TarifDataProvider = tarifDataProvider;
            _PriceItemDataProvider = priceItemDataProvider;
            _Mapper = mapper;
        }

        /// <summary>
        /// Получить все тарифные планы
        /// </summary>
        public async Task<IEnumerable<TarifDTO>> GetTariffPlansAsync() 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выбрать тарифный план по идентификатору тарифа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public async Task<TarifDTO> ChoiceTariffPlanAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Посмотреть тарифный план по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public async Task<TarifDTO> GetTariffPlanByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        public async Task<Guid> CreateTariffPlanAsync(TarifDTO tarif)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Редактировать тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        public async Task<bool> EditTariffPlanAsync(TarifDTO tarif)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить тарифный план
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public async Task<bool> RemoveTariffPlanAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Архивирование тарифного плана
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public async Task<bool> ArchivingTariffPlanAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // Работа с услугами

        /// <summary>
        /// Заказать услугу
        /// </summary>
        public async Task<bool> OrderPriceItemAsync(Guid serviceId, Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Посмотреть все услуги
        /// </summary>
        public async Task<PageDTOs<PriceItemDTO>> GetAllPriceItemsAsync(Filter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Посмотреть все услуги пользователя по идентификатору
        /// </summary>
        public async Task<IEnumerable<PriceItemDTO>> GetAllPriceItemsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Посмотреть все текущие заказанные услуги, статусы по услугам 
        /// </summary>
        public async Task<IEnumerable<PriceItemDTO>> GetOpenPriceItemsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Посмотреть всю историю заказов услуг
        /// </summary>
        public async Task<IEnumerable<PriceItemDTO>> GetPriceItemsHistoryAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    

        /// <summary>
        /// Посмотреть информацию об услуге
        /// </summary>
        /// <param name="id">Идентификатор услуги</param>
        public async Task<PriceItemDTO> GetPriceItemById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
