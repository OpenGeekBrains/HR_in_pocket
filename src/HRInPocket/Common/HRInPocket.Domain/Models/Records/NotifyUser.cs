namespace HRInPocket.Domain.Models.Records
{
    public class NotifyUser
    {
        public readonly string Email;

        public NotifyUser(string email)
        {
            Email = email;
        }
        
        public bool EmailNotify { get; set; }
        public bool SmsNotify { get; set; }
        public bool PushNotify { get; set; }
    }
}