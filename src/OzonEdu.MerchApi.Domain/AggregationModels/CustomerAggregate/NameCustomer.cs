using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate
{
    public class NameCustomer : ValueObject
    {
        public string Value { get; }
        
        public NameCustomer(string val)
        {
            Value = val;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}