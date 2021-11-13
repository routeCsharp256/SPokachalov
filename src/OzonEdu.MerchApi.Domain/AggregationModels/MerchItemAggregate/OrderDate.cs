using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public sealed class OrderDate:ValueObject
    {
        public DateTime Value { get; }
        
        public OrderDate(DateTime val)
        {
            Value = val;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}