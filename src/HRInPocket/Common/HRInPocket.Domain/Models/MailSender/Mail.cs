namespace HRInPocket.Domain.Models.MailSender
{
    /// <summary>
    /// Модель письма
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Тема письма </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма (html и css, текстовая части) </summary>
        public string Body { get; set; }

        /// <summary>
        /// Адрес лектронной почты получателя </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Ссылка с переменными на действие </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Url для отписки от рассылки </summary>
        public string UnsubscribeUrl { get; set; }
    }
}
