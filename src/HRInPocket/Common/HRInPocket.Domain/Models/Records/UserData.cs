using System;

namespace HRInPocket.Domain.Models.Records
{
    public readonly struct UserData : IEquatable<UserData>
    {
        public UserData(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        
        public readonly string email;
        public readonly string password;

        public override bool Equals(object obj) => obj is UserData data && Equals(data);
        public bool Equals(UserData other) => email == other.email && password == other.password;
        public override int GetHashCode() => HashCode.Combine(email, password);

        public static bool operator ==(UserData left, UserData right) => left.Equals(right);
        public static bool operator !=(UserData left, UserData right) => !(left == right);
    }
}