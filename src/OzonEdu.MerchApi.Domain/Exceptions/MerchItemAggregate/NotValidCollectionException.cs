using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchItemAggregate
{
    public class NotValidCollectionException : Exception
    {
        public NotValidCollectionException(string message) : base(message)
        {
        }

        public NotValidCollectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}