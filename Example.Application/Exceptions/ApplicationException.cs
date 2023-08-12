using System;

namespace Example.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string businessMessage)
               : base(businessMessage)
        {
        }

        public ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
