using System.Collections.Generic;

using HRInPocket.Domain.Models.Records;

namespace HRInPocket.Services.Services
{
    public class NotifyService
    {
        private static readonly List<NotifyUser> Notify = new List<NotifyUser>();
        
        public IEnumerable<NotifyUser> GetNotifyUsers()
        {
            for (var i = 0; i < Notify.Count; i++)
                yield return Notify[i];
        }

        public void NotifyMe(NotifyUser notify)
        {
            var user = Notify.Find(n => n.Email == notify.Email);
            if (user is null) Notify.Add(notify);
            var index = Notify.IndexOf(user);
            Notify[index] = notify;
        }

        public bool UnsubscribeEmail(string email)
        {
            var notify = Notify.Find(n => n.Email == email);
            if (notify is null) return false;
            notify.EmailNotify = false;
            return true;
        }
    }
}