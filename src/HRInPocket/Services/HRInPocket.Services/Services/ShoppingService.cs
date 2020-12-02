using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Entities.Data;
using HRInPocket.Domain.Filters;
using HRInPocket.Interfaces;
using HRInPocket.Interfaces.Services;

using Microsoft.EntityFrameworkCore;

namespace HRInPocket.Services.Services
{
    public class ShoppingService : IShoppingService
    {
        /// <summary>
        /// Провайдер данных </summary>
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
        public async Task<IEnumerable<TarifDTO>> GetTariffPlansAsync() => 
            (await _TarifDataProvider.GetAllAsync())
            .Select(t => _Mapper.Map<TarifDTO>(t));

        /// <summary>
        /// Выбрать тарифный план по идентификатору тарифа
        /// </summary>
        /// <param name="id">Идентификатор тарифа</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public Task<TarifDTO> ChoiceTariffPlanAsync(Guid id, Guid userId) => throw new NotImplementedException();

        /// <summary>
        /// Посмотреть тарифный план по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public async Task<TarifDTO> GetTariffPlanByIdAsync(Guid id) =>
            _Mapper.Map<TarifDTO>(await _TarifDataProvider.GetByIdAsync(id));

        /// <summary>
        /// Создать новый тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        public async Task<Guid> CreateTariffPlanAsync(TarifDTO tarif) =>
            await _TarifDataProvider.CreateAsync(_Mapper.Map<Tarif>(tarif));

        /// <summary>
        /// Редактировать тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        public async Task<bool> EditTariffPlanAsync(TarifDTO tarif) =>
            await _TarifDataProvider.EditAsync(_Mapper.Map<Tarif>(tarif));

        /// <summary>
        /// Удалить тарифный план
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public async Task<bool> RemoveTariffPlanAsync(Guid id) =>
            await _TarifDataProvider.RemoveAsync(id);

        /// <summary>
        /// Архивирование тарифного плана
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        public Task<bool> ArchivingTariffPlanAsync(Guid id) => throw new NotImplementedException();

        // Работа с услугами

        /// <summary>
        /// Заказать услугу
        /// </summary>
        public Task<bool> OrderPriceItemAsync(Guid serviceId, Guid userId) => throw new NotImplementedException();

        /// <summary>
        /// Посмотреть все услуги
        /// </summary>
        public async Task<PagePriceItemDTO> GetAllPriceItemsAsync(PriceItemFilter filter)
        {
            var query = _PriceItemDataProvider.GetQueryable();

            if (filter != null)
            {
                /*Логика фильтрации после понимания структуры фильтров*/
            }

            var count = await query.CountAsync();

            query = query
                .Skip((filter.Pages.PageNumber - 1) * filter.Pages.PageSize)
                .Take(filter.Pages.PageSize);

            return new PagePriceItemDTO
            {
                Companies = query.Select(q => _Mapper.Map<PriceItemDTO>(q)),
                TotalCount = count
            };
        }

        /// <summary>
        /// Посмотреть все услуги пользователя по идентификатору
        /// </summary>
        public Task<IEnumerable<PriceItemDTO>> GetAllPriceItemsAsync(Guid userId) => throw new NotImplementedException();

        /// <summary>
        /// Посмотреть все текущие заказанные услуги, статусы по услугам 
        /// </summary>
        public Task<IEnumerable<PriceItemDTO>> GetOpenPriceItemsAsync(Guid userId) => throw new NotImplementedException();

        /// <summary>
        /// Посмотреть всю историю заказов услуг
        /// </summary>
        public Task<IEnumerable<PriceItemDTO>> GetPriceItemsHistoryAsync(Guid userId) => throw new NotImplementedException();

        /// <summary>
        /// Посмотреть информацию об услуге
        /// </summary>
        /// <param name="id">Идентификатор услуги</param>
        public async Task<PriceItemDTO> GetPriceItemById(Guid id) =>
            _Mapper.Map<PriceItemDTO>(await _PriceItemDataProvider.GetByIdAsync(id));
    }
}
