using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Exception;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Contracts;


namespace OzonEdu.Infrastructure.Handlers.MerchItemAggregate
{
    public class GetIssuedMerchPacksQueryHandler : IRequestHandler<GetIssuedMerchPacksQuery, List<MerchItem>>
    {
        public readonly IMerchItemRepository _merchItemRepository;
        public readonly IUnitOfWork _unitOfWork;

        public GetIssuedMerchPacksQueryHandler(IMerchItemRepository merchItemRepository,IUnitOfWork unitOfWork)
        {
            _merchItemRepository = merchItemRepository ??
                                   throw new ArgumentNullException($"{nameof(merchItemRepository)}");
            _unitOfWork = unitOfWork ?? 
                          throw new ArgumentNullException($"{nameof(unitOfWork)}");
          
        }

        public async Task<List<MerchItem>> Handle(GetIssuedMerchPacksQuery query, CancellationToken cancellationToken)
        {
             return  await _merchItemRepository.FindByCustomerIdAsync(query.Id);
        }
    }
}