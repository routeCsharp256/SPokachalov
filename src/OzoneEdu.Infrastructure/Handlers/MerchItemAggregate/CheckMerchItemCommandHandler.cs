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
using OzonEdu.MerchApi.Domain.Contracts;


namespace OzonEdu.Infrastructure.Handlers.MerchItemAggregate
{
    public class CheckMerchItemCommandHandler : IRequestHandler<CheckMerchItemCommand, bool>
    {
        public readonly IMerchItemRepository _merchItemRepository;
        public readonly IMerchPackRepository _merchPackRepository;
        public readonly IUnitOfWork _unitOfWork;

        public CheckMerchItemCommandHandler(IMerchItemRepository merchItemRepository,
            IMerchPackRepository merchPackRepository,
            IUnitOfWork unitOfWork)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
          
            _merchPackRepository = merchPackRepository ??
                                   throw new ArgumentNullException($"{nameof(merchPackRepository)}");
            _unitOfWork = unitOfWork ?? 
                          throw new ArgumentNullException($"{nameof(unitOfWork)}");
        }

        public async Task<bool> Handle(CheckMerchItemCommand merchItem, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            
            // var stockInDb = await _stockItemRepository.FindBySkuAsync(new Sku(request.Sku), cancellationToken);
            // if (stockInDb is not null)
            //     throw new Exception($"Stock item with sku {request.Sku} already exist");
            
            var merchPack = await _merchPackRepository.FindByIdAsync(merchItem.MerchPackTypeId);
            var someTypeMerch = (await _merchItemRepository.FindByCustomerIdAsync(merchItem.MerchCustomerId))
                .Where(x => x.Pack.Equals(merchPack)).OrderByDescending(x => x.ConfirmDate)
                .FirstOrDefault();
            if (someTypeMerch != null && DateTime.Now.AddYears(-1) <= someTypeMerch.ConfirmDate.Value)
                throw new AlreadyIssuedException("Набор уже выдавался");
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}