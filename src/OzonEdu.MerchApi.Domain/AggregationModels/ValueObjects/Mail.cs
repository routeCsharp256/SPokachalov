using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects
{
    public  class Mail : ValueObject
    {
        public string Value { get; }
        
        public Mail(string val)
        {
            Value = val;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}