using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.CustomerAggregate
{
    public sealed class NotValidMailException : Exception
    {
        public NotValidMailException(string message) : base(message)
        {
        }

        public NotValidMailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}