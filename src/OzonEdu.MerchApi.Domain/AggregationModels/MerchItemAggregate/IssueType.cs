using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public sealed class IssueType : Enumeration
    {
        public static IssueType Auto = new(1, nameof(Auto));
        public static IssueType Manual = new(2, nameof(Manual));
        public IssueType(int id, string name) : base(id, name)
        {
        }
    }
}