using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.DTO.Pages;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Repository
{
    public interface IShoppingService
    {
        /// <summary>
        /// Получить все тарифные планы
        /// </summary>
        Task<IEnumerable<TarifDTO>> GetTariffPlansAsync();

        /// <summary>
        /// Выбрать тарифный план по идентификатору тарифа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<TarifDTO> ChoiceTariffPlanAsync(Guid id, Guid userId);

        /// <summary>
        /// Посмотреть тарифный план по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<TarifDTO> GetTariffPlanByIdAsync(Guid id);
        
        /// <summary>
        /// Создать новый тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        Task<Guid> CreateTariffPlanAsync(TarifDTO tarif);

        /// <summary>
        /// Редактировать тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        Task<bool> EditTariffPlanAsync(TarifDTO tarif);

        /// <summary>
        /// Удалить тарифный план
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<bool> RemoveTariffPlanAsync(Guid id);




        /// <summary>
        /// Архивирование тарифного плана
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<bool> ArchivingTariffPlanAsync(Guid id);

        // Работа с услугами

        /// <summary>
        /// Заказать услугу
        /// </summary>
        Task<bool> OrderPriceItemAsync(Guid serviceId, Guid userId);

        /// <summary>
        /// Посмотреть все услуги
        /// </summary>
        Task<PageDTOs<PriceItemDTO>> GetAllPriceItemsAsync(Filter filter);

        /// <summary>
        /// Посмотреть все услуги пользователя по идентификатору
        /// </summary>
        Task<IEnumerable<PriceItemDTO>> GetAllPriceItemsAsync(Guid userId);

        /// <summary>
        /// Посмотреть все текущие заказанные услуги, статусы по услугам 
        /// </summary>
        Task<IEnumerable<PriceItemDTO>> GetOpenPriceItemsAsync(Guid userId);

        /// <summary>
        /// Посмотреть всю историю заказов услуг
        /// </summary>
        Task<IEnumerable<PriceItemDTO>> GetPriceItemsHistoryAsync(Guid userId);

        /// <summary>
        /// Посмотреть информацию об услуге
        /// </summary>
        /// <param name="id">Идентификатор услуги</param>
        Task<PriceItemDTO> GetPriceItemById(Guid id);
    }
}
