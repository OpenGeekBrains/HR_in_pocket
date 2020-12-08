namespace HRInPocket.Infrastructure.Models
{
    public class LogEvents
    {
        public const int GenerateItems = 1000;
        public const int ListItems     = 1001;
        public const int GetItem       = 1002;
        public const int InsertItem    = 1003;
        public const int UpdateItem    = 1004;
        public const int DeleteItem    = 1005;

        public const int TestItem      = 3000;

        public const int GetItemNotFound    = 4000;
        public const int UpdateItemNotFound = 4001;

        public const int AccountRegistration = 5000;
        public const int AccountLogin = 5001;
        public const int AccountLogout = 5002;
        
        public const int AccountRegistrationFailure = 5010;
        public const int AccountLoginFailure = 5011;
        public const int AccountLogoutFailure = 5012;

        public const int MailSending = 9000;
    }
}