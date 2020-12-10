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
        void Send(Mail mail);

        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task SendAsync(Mail mail);

        /// <summary>
        /// Рассылка писем
        /// </summary>
        /// <param name="mails">Коллекция моделей писем</param>
        Task SendByListAsync(IEnumerable<Mail> mails);

        /// <summary>
        /// Отправить письмо подтверждения электронной почты
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task SendEmailConfirmation(Mail mail);

        /// <summary>
        /// Отправить письмо для обновления пароля
        /// </summary>
        /// <param name="mail">Модель письма</param>
        Task SendPasswordUpdate(Mail mail);
    }
}
