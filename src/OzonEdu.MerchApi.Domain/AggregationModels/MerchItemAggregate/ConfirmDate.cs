using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public sealed class ConfirmDate :ValueObject
    {
        public DateTime Value { get; }
        
        public ConfirmDate(DateTime val)
        {
            Value = val;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}