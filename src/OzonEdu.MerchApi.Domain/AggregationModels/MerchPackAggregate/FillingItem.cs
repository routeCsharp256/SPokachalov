using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class FillingItem : Entity
    {
        public override int Id { get; protected set; }
        public FillingItemType Type { get; }

        public IReadOnlyCollection<Sku> CollectionSku { get; private set; }

        public FillingItem(FillingItemType type, IReadOnlyCollection<Sku> collectionSku)
        {
            Type = type;
            CollectionSku = collectionSku;
        }

        public FillingItem(int id, FillingItemType type, IReadOnlyCollection<Sku> collectionSku)
        {
            Id = id;
            Type = type;
            CollectionSku = collectionSku;
        }
    }
}