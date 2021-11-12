using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Exception;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;


namespace OzonEdu.Infrastructure.Handlers.DeliveryRequestAggregate
{
    public class CheckMerchItemCommandHandler : IRequestHandler<CheckMerchItemCommand, bool>
    {
        public readonly IMerchItemRepository _merchItemRepository;
        public readonly IMerchPackRepository _merchPackRepository;

        public CheckMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
            IMerchPackRepository merchPackRepository)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
          
            _merchPackRepository = merchPackRepository ??
                                   throw new ArgumentNullException($"{nameof(merchPackRepository)}");
        }

        public async Task<bool> Handle(CheckMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            var merchPack = await _merchPackRepository.FindByIdAsync(merchItem.MerchPackTypeId);
            var someTypeMerch = (await _merchItemRepository.FindByCustomerIdAsync(merchItem.MerchCustomerId))
                .Where(x => x.Pack.Equals(merchPack)).OrderByDescending(x => x.ConfirmDate)
                .FirstOrDefault();
            if (DateTime.Now.AddYears(-1) <= someTypeMerch.ConfirmDate.Value)
                throw new AlreadyIssuedException("Набор уже выдавался");
            return true;
        }
    }
}