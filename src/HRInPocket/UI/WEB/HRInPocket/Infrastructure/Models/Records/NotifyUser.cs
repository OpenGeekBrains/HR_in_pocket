namespace HRInPocket.Infrastructure.Models.Records
{
    public record NotifyUser(string email)
    {
        public bool EmailNotify { get; set; }
        public bool SmsNotify { get; set; }
        public bool PushNotify { get; set; }
    }
}