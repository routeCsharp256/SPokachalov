using System;

namespace Infrastructure.Exception
{
    public class AlreadyIssuedException : System.Exception
    {
        public AlreadyIssuedException(string message) : base(message)
        {
        }

        public AlreadyIssuedException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}