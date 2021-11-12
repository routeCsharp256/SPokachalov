using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.Events;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class SetToWaitMerchItemDomainEventHandler : INotificationHandler<SetToWaitMerchItemDomainEvent>
    {
        public Task Handle(SetToWaitMerchItemDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Item.IssueType.Equals(IssueType.Auto))
            {
                // Отправить маил о том что закончились поставки
                 /// notification.Item.Customer.MentorMail
            }

            return new Task(() => { });
        }
    }
}