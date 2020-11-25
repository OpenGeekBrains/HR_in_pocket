using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.Models.MailSender;

namespace HRInPocket.Interfaces.Services
{
    public interface IMailSenderService
    {
        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <param name="mail">Модель письма</param>
        bool Send(Mail mail);

        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task<bool> SendAsync(Mail mail);

        /// <summary>
        /// Рассылка писем
        /// </summary>
        /// <param name="mails">Коллекция моделей писем</param>
        Task<bool> SendByListAsync(IEnumerable<Mail> mails);

        /// <summary>
        /// Отправить письмо подтверждения электронной почты
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task<bool> SendEmailConfirmation(Mail mail);

        /// <summary>
        /// Отправить письмо для обновления пароля
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task<bool> SendPasswordUpdate(Mail mail);
    }
}
