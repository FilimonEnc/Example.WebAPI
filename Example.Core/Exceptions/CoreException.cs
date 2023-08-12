using System;

namespace Example.Core.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException(string businessMessage)
            : base(businessMessage)
        {
        }

        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
