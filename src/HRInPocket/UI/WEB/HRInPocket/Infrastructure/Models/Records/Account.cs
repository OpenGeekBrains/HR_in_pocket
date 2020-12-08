using System;

namespace HRInPocket.Infrastructure.Models.Records
{
    public record Account(UserData Data)
    {
        public Guid Token { get; } = Guid.NewGuid();

        public bool IsLoggedIn { get; set; }
    }
}