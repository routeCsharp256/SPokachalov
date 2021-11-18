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
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure;


namespace OzonEdu.Infrastructure.Handlers.MerchItemAggregate
{
    public class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, int>
    {
        public readonly IMerchItemRepository _merchItemRepository;
        public readonly IMerchItemCustomerRepository _customerRepository;
        public readonly IMerchPackRepository _merchPackRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CreateMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
            IMerchItemCustomerRepository customerRepository,
            IMerchPackRepository merchPackRepository,
            IUnitOfWork unitOfWork)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
            _customerRepository = customerRepository ??
                                  throw new ArgumentNullException($"{nameof(customerRepository)}");
            _merchPackRepository = merchPackRepository ??
                                   throw new ArgumentNullException($"{nameof(merchPackRepository)}");
            _unitOfWork = unitOfWork ?? 
                          throw new ArgumentNullException($"{nameof(unitOfWork)}");

        }

        public async Task<int> Handle(CreateMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            var item = new MerchItem(
                pack: await _merchPackRepository.FindByIdAsync(merchItem.MerchPackTypeId),
                customer: await _customerRepository.FindByIdAsync(merchItem.MerchCustomerId),
                issueType: IssueType.GetAll<IssueType>().FirstOrDefault(x => x.Id == merchItem.IssueTypeId),
                skuList: merchItem.Sku.Select(it => new Sku(it)).ToList()); 
            item = await _merchItemRepository.CreateAsync(item, cancellationToken);
            await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            return item.Id;
        }
    }
}