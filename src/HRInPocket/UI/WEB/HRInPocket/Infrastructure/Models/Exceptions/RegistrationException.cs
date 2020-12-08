using HRInPocket.Infrastructure.Models.Records;

namespace HRInPocket.Infrastructure.Models.Exceptions
{
    public class RegistrationException : AuthorizationException
    {
        public readonly UserData User;

        public RegistrationException(UserData user) : base($"User with {user.email} already exist") => User = user;
    }
}