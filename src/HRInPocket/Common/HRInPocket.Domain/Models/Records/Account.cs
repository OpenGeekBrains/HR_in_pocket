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
        
        public readonly UserData Data;
        
        public readonly Guid Token = Guid.NewGuid();

        public bool IsLoggedIn { get; set; }
    }
}