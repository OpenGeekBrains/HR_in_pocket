using System;

namespace HRInPocket.Infrastructure.Models.Records
{
    public record Account(UserData Data)
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Guid Token { get; } = Guid.NewGuid();

        public bool IsLoggedIn { get; set; }
    }
}