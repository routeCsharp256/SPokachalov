using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchPack:Entity
    {
        public override int Id { get; protected set; }
        public MerchType Type { get; }

        public IReadOnlyCollection<FillingItem> FillingItems { get; private set; }

        public MerchPack(MerchType type, IReadOnlyCollection<FillingItem> items)
        {
            Type = type;
            SetItems(items);
        }

        public MerchPack(int id, MerchType type, IReadOnlyCollection<FillingItem> items)
        {
            Id = id;
            Type = type;
            SetItems(items);
        }
        private void SetItems(IReadOnlyCollection<FillingItem> items)
        {
            FillingItems = items;
        }
        
        public IReadOnlyCollection<Sku> GetAvailableSkuCollection()
        {
            List<Sku> collection = new List<Sku>();
            foreach (var items in FillingItems)
                collection.AddRange(items.CollectionSku);
            return collection;
        }

    }
}