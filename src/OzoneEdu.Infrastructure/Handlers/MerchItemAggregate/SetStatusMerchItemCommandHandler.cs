using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Contracts;


namespace OzonEdu.Infrastructure.Handlers.MerchItemAggregate
{
    public class SetStatusMerchItemCommandHandler : IRequestHandler<SetStatusMerchItemCommand, bool>
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMerchItemRepository _merchItemRepository;

        public SetStatusMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
            IMerchItemCustomerRepository customerRepository,
            IMerchPackRepository merchPackRepository,
            IUnitOfWork unitOfWork)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
            _unitOfWork = unitOfWork ?? 
                          throw new ArgumentNullException($"{nameof(unitOfWork)}");
        }

        public async Task<bool> Handle(SetStatusMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            var item = await _merchItemRepository.FindByIdAsync((int)merchItem.MerchId);
            if (merchItem.Status)
                item.SetConfirmDate(DateTime.Now);
            else
                item.SetToWait();

            await _merchItemRepository.CreateAsync(item, cancellationToken);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}