using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.DTO;
using HRInPocket.Domain.Filters;

namespace HRInPocket.Interfaces.Services
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
        Task<TarifDTO> ChoiceTariffPlanAsync(long id, Guid userId);

        /// <summary>
        /// Посмотреть тарифный план по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<TarifDTO> GetTariffPlanByIdAsync(long id);
        
        /// <summary>
        /// Создать новый тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        Task<long> CreateTariffPlanAsync(TarifDTO tarif);

        /// <summary>
        /// Редактировать тарифный план
        /// </summary>
        /// <param name="tarif">Модель тарифного плана</param>
        Task<bool> EditTariffPlanAsync(TarifDTO tarif);

        /// <summary>
        /// Удалить тарифный план
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<bool> RemoveTariffPlanAsync(long id);

        /// <summary>
        /// Архивирование тарифного плана
        /// </summary>
        /// <param name="id">Идентификатор тарифного плана</param>
        Task<bool> ArchivingTariffPlanAsync(long id);

        // Работа с услугами

        /// <summary>
        /// Заказать услугу
        /// </summary>
        Task<bool> OrderServiceAsync(long serviceId, Guid userId);

        /// <summary>
        /// Посмотреть все услуги
        /// </summary>
        Task<PageServiceDTO> GetAllServicesAsync(ServiceFilter filter);

        /// <summary>
        /// Посмотреть все услуги пользователя по идентификатору
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetAllServicesAsync(Guid userId);

        /// <summary>
        /// Посмотреть все текущие заказанные услуги, статусы по услугам 
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetOpenServicesAsync(Guid userId);

        /// <summary>
        /// Посмотреть всю историю заказов услуг
        /// </summary>
        Task<IEnumerable<ServiceDTO>> GetServiceHistoryAsync(Guid userId);

        /// <summary>
        /// Посмотреть информацию об услуге
        /// </summary>
        /// <param name="id">Идентификатор услуги</param>
        Task<ServiceDTO> GetServiceById(long id);
    }
}
