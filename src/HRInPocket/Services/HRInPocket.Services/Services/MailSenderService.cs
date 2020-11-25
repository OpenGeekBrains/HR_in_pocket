using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HRInPocket.Domain.Models.MailSender;
using HRInPocket.Interfaces.Services;

namespace HRInPocket.Services.Services
{
    public class MailSenderService : IMailSenderService
    {
        public MailSenderService() { }

        public bool Send(Mail mail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(Mail mail)
        {
            throw new NotImplementedException();
        }

        public bool SendByList(IEnumerable<Mail> mail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendByListAsync(IEnumerable<Mail> mail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailConfirmation(Mail mail)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendPasswordUpdate(Mail mail)
        {
            throw new NotImplementedException();
        }
    }
}
