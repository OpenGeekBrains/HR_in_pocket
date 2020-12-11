using System;

namespace HRInPocket.Domain.Models.Records
{
    public class Account
    {
        public Guid Id { get; } = Guid.NewGuid();
        
        public Account(UserData data)
        {
            Data = data;
        }
        
        public UserData Data { get; set; }
        
        public Guid Token { get; } = Guid.NewGuid();

        public bool IsLoggedIn { get; set; }
    }
}