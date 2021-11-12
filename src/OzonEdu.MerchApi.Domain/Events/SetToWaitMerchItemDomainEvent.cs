using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public sealed class SetToWaitMerchItemDomainEvent :  INotification
    {
        public MerchItem Item { get; }

        public SetToWaitMerchItemDomainEvent(MerchItem item)
        {
            Item = item;
        }
    }
}