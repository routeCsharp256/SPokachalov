using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.Events;

namespace OzonEdu.StockApi.Infrastructure.Handlers
{
    public class СonfirmMerchItemEventHandler : INotificationHandler<ConfirmMerchItemDomainEvent>
    {
        public Task Handle(ConfirmMerchItemDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Item.IssueType == IssueType.Auto){
            // Отправить e-mail сотруднику, что ему необходимо подойти к HR для получения мерча
            //notification.Item.Customer.MentorMail;
            }
            else if (notification.Item.IssueType == IssueType.Manual)
            {
                // Отправить e-mail сотруднику, что ему одобрили получения мерча
                // notification.Item.Customer.Mail;
            }

            throw new System.NotImplementedException();
        }
    }
}