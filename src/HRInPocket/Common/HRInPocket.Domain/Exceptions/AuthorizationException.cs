using System;

namespace HRInPocket.Domain.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message, Exception innerException = null) : base(message, innerException)
        {

        }
    }
}