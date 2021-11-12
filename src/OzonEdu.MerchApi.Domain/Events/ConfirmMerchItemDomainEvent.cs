using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    /// <summary>
    /// Событие подтверждения запроса на выдачу набора товаров
    /// </summary>
    public sealed class ConfirmMerchItemDomainEvent : INotification
    {
        public MerchItem Item { get; }

        public ConfirmMerchItemDomainEvent(MerchItem item)
        {
            Item = item;
        }
    }
}