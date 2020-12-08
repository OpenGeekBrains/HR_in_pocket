using System;

namespace HRInPocket.Infrastructure.Models.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message, Exception innerException = null) : base(message, innerException)
        {

        }
    }
}