using System.Threading.Tasks;

namespace HRInPocket.Interfaces.Services
{
    /// <summary>
    /// Сервис оплаты услуг
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Оплатить услугу
        /// </summary>
        Task PaymentAsync();

        /// <summary>
        /// Расчитать скидку
        /// </summary>
        Task CalculationDiscountsAsync();
    }
}
