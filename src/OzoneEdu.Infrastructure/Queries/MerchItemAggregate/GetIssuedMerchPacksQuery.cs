using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;

namespace OzonEdu.Infrastructure.Queries.StockItemAggregate
{
    /// <summary>
    /// Полуить выданные наборы товаров 
    /// </summary>
    public class GetIssuedMerchPacksQuery : IRequest<List<MerchItem>>
    {
        public int Id { get; set; }
    }
}