using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.Collection
{
    public sealed class EmptyCollectionException : Exception
    {
        public EmptyCollectionException(string message) : base(message)
        {
        }

        public EmptyCollectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}