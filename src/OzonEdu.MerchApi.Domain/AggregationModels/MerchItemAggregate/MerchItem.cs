using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Exceptions.Collection;
using OzonEdu.MerchApi.Domain.Exceptions.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.StockApi.Domain.AggregatesModels.DeliveryRequestAggregate;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public sealed class MerchItem : Entity
    {
        public override int Id { get; protected set; }
        public MerchPack Pack { get;  private set;}
        public IReadOnlyCollection<Sku> SkuList { get; private set; }
        public IssueType IssueType { get; }
        public MerchCustomer Customer { get; private set; }
        public OrderDate OrderDate { get; }      
        public ConfirmDate ConfirmDate { get; private set; }

        public MerchItemStatus MerchItemStatus { get; private set; }

        public MerchItem(MerchCustomer customer, MerchPack pack, IReadOnlyCollection<Sku> skuList, IssueType issueType)
        {
            Pack = pack;
            SetSkuCollection(skuList);
            IssueType = issueType;
            Customer = customer;
            OrderDate = new OrderDate(DateTime.Now);
        }
        
        public MerchItem(int id, MerchCustomer customer, MerchPack pack, IReadOnlyCollection<Sku> skuList, IssueType issueType)
        {
            Id = id;
            Pack = pack;
            SetSkuCollection(skuList);
            IssueType = issueType;
            Customer = customer;
            OrderDate = new OrderDate(DateTime.Now);
        }
        
        public void SetSkuCollection(IReadOnlyCollection<Sku> skuList)
        {
            if (skuList.Count == 0)
                throw new EmptyCollectionException("Пустой набор товаров");

            var collection = Pack.GetAvailableSkuCollection();
            foreach (var sku in skuList)
                if (!collection.Any(x => x.Equals(sku)))
                    throw new NotValidCollectionException("Не соответствует типу набора товаров");
            SkuList = skuList;
        }
        
        public void SetConfirmDate(DateTime date)
        {
            var сonfirmMerchItemDomainEvent = new ConfirmMerchItemDomainEvent(this);
            this.AddDomainEvent(сonfirmMerchItemDomainEvent);
            ConfirmDate = new ConfirmDate(DateTime.Now);
            MerchItemStatus = MerchItemStatus.Done;
        }

        public void SetToWait()
        {
            var setToWaitMerchItemDomainEvent = new SetToWaitMerchItemDomainEvent(this);
            this.AddDomainEvent(setToWaitMerchItemDomainEvent);
            MerchItemStatus = MerchItemStatus.InWork;
        }
        
    }
}