using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;


namespace OzonEdu.Infrastructure.Handlers.MerchItemAggregate
{
    public class SetStatusMerchItemCommandHandler : IRequestHandler<SetStatusMerchItemCommand, bool>
    {
        public readonly IMerchItemRepository _merchItemRepository;

        public SetStatusMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
            IMerchItemCustomerRepository customerRepository,
            IMerchPackRepository merchPackRepository)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
        }

        public async Task<bool> Handle(SetStatusMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            var item = await _merchItemRepository.FindByIdAsync((int)merchItem.MerchId);
            if (merchItem.Status)
                item.SetConfirmDate(DateTime.Now);
            else
                item.SetToWait();

            await _merchItemRepository.CreateAsync(item, cancellationToken);
            return await _merchItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}