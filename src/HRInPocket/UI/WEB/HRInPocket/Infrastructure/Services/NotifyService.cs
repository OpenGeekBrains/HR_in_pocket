using System.Collections.Generic;

using HRInPocket.Infrastructure.Models.Records;

namespace HRInPocket.Infrastructure.Services
{
    public class NotifyService
    {
        private static readonly List<NotifyUser> Notify = new();
        
        public IEnumerable<NotifyUser> GetNotifyUsers()
        {
            for (var i = 0; i < Notify.Count; i++)
                yield return Notify[i];
        }

        public void NotifyMe(NotifyUser notify)
        {
            var user = Notify.Find(n => n.email == notify.email);
            if (user is null) Notify.Add(notify);
            var index = Notify.IndexOf(user);
            Notify[index] = notify;
        }

        public bool UnsubscribeEmail(string email)
        {
            var notify = Notify.Find(n => n.email == email);
            if (notify is null) return false;
            notify.EmailNotify = false;
            return true;
        }
    }
}