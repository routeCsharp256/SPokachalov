using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class FillingItem : Entity
    {
        public FillingItemType Type { get; }
        
        public IReadOnlyCollection<Sku> CollectionSku { get; private set; }

        public FillingItem(FillingItemType type, IReadOnlyCollection<Sku> collectionSku)
        {
            Type = type;
            CollectionSku = collectionSku;
        }
    }
}