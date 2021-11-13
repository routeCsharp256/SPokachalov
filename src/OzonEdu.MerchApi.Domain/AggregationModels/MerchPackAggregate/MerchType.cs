namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchType
    {
        public MerchPackType Type { get; }

        public MerchType(MerchPackType type) => Type = type;
    }
}