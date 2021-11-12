using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;


namespace OzonEdu.Infrastructure.Handlers.DeliveryRequestAggregate
{
    public class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, bool>
    {
        public readonly IMerchItemRepository _merchItemRepository;
        public readonly IMerchItemCustomerRepository _customerRepository;
        public readonly IMerchPackRepository _merchPackRepository;
        
        public CreateMerchItemCommandHandler(IMerchItemRepository merchItemRepository, 
            IMerchItemCustomerRepository customerRepository,
            IMerchPackRepository merchPackRepository)
        {
            _merchItemRepository = merchItemRepository ?? 
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
            _customerRepository = customerRepository ?? 
                                  throw new ArgumentNullException($"{nameof(customerRepository)}");
            _merchPackRepository = merchPackRepository ??
                                   throw new ArgumentNullException($"{nameof(merchPackRepository)}");

        }
        
        public async Task<bool> Handle(CreateMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            var item = new MerchItem(
                pack: await _merchPackRepository.FindByIdAsync(merchItem.MerchPackTypeId),
                customer:await _customerRepository.FindByIdAsync(merchItem.MerchCustomerId),
                issueType: IssueType.GetAll<IssueType>().FirstOrDefault(x=>x.Id == merchItem.IssueTypeId),
                skuList:merchItem.Sku.Select(it => new Sku(it)).ToList());
            if (merchItem.IsDone)
                item.SetConfirmDate(DateTime.Now);
            else
                item.SetToWait();

            await _merchItemRepository.CreateAsync(item, cancellationToken);
            return await _merchItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}