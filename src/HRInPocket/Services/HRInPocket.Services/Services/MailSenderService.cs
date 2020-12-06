using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HRInPocket.Domain.Models.MailSender;
using HRInPocket.Interfaces.Services;

using MailKit.Net.Smtp;

using Microsoft.Extensions.Logging;

using MimeKit;

namespace HRInPocket.Services.Services
{
    public class MailSenderService : IMailSenderService
    {
        /// <summary>
        /// Сервис логирования </summary>
        private readonly ILogger<MailSenderService> _Logger;

        public MailSenderService(ILogger<MailSenderService> logger) => _Logger = logger;

        public void Send(Mail mail) => SendAsync(mail).ConfigureAwait(false);

        public async Task SendAsync(Mail mail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(MailInfo.Name, MailInfo.Address));
            emailMessage.To.Add(new MailboxAddress(MailInfo.RecipientName, mail.RecipientEmail));
            emailMessage.Headers.Add(HeaderId.ListUnsubscribe, mail.UnsubscribeUrl);
            emailMessage.Subject = mail.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = GetTextBody(mail.Body),
                HtmlBody = GetHtmlBody(mail.Body)
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(MailInfo.Server, MailInfo.Port);
                //Magic string for honest mailing / Don't removing
                client.AuthenticationMechanisms.Remove(MailInfo.MagicWord);
                await client.AuthenticateAsync(MailInfo.Login, MailInfo.Pass);

                if (!mail.UnsubscribeUrl.Contains(MailInfo.Local))
                {
                    await client.SendAsync(emailMessage);
                    ResponseHandler();
                }

                await client.DisconnectAsync(true);
                _Logger.LogInformation(MailInfo.Success);
            }
            catch (Exception e)
            {
                _Logger.LogInformation(string.Format("{0} - {1}", MailInfo.Bad, e.Message));
            }
        }

        public void SendByList(IEnumerable<Mail> mail) => SendByListAsync(mail).ConfigureAwait(false);

        public async Task SendByListAsync(IEnumerable<Mail> mail)
        {
            foreach (var m in mail)
                await SendAsync(m);
        }

        public Task SendEmailConfirmation(Mail mail) => throw new NotImplementedException();

        public Task SendPasswordUpdate(Mail mail) => throw new NotImplementedException();


        private string GetTextBody(string body) => body;
        private string GetHtmlBody(string body) => body;

        private void ResponseHandler() {}
    }

    public static class MailInfo
    {
        public const string Name = "HRInPocket"; 
        public const string Address = "";
        public const string RecipientName = "";
        public const string Server = "";
        public const int Port = 0;
        public const string Login = "";
        public const string Pass = "";
        public const string MagicWord = "XOAUTH2";
        public const string Local = "local";
        public const string Success = "[Mailing-Success]";
        public const string Bad = "[Mailing-Bad]";
    }
}
