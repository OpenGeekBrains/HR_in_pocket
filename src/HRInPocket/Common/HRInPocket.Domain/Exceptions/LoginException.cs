namespace HRInPocket.Domain.Exceptions
{
    public class LoginException : AuthorizationException
    {
        public readonly string BadParameter;

        public LoginException(string message, string badParameter) : base(message) => BadParameter = badParameter;
    }
}