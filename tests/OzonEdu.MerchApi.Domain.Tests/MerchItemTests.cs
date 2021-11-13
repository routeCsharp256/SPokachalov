using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Exceptions.Collection;
using OzonEdu.MerchApi.Domain.Exceptions.MerchItemAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests
{
    public sealed class MerchItemTests
    {
        [Fact]
        public void CreateMerchItem()
        {
            var merchItem = new MerchItem(
                new MerchCustomer(new MailCustomer("qwerty@ya.ru"), new NameCustomer("Иванов И.И.")),
                new MerchPack(new MerchType(MerchPackType.VeteranPack), new List<FillingItem>()
                {
                    new FillingItem(FillingItemType.Notepad,
                        (new List<Sku>() {new Sku(123), new Sku(321)}).AsReadOnly()),
                    new FillingItem(FillingItemType.Notepad,
                        (new List<Sku>() {new Sku(666), new Sku(999)}).AsReadOnly()),
                }.AsReadOnly()),
                new List<Sku>() {new Sku(666), new Sku(999), new Sku(123), new Sku(321)}.AsReadOnly(),
                IssueType.Auto);

            Assert.NotNull(merchItem);
        }

        [Fact]
        public void NotValidSkuListMerchItem()
        {
            Assert.Throws<NotValidCollectionException>(() =>
            {
                var merchItem = new MerchItem(
                    new MerchCustomer(new MailCustomer("qwerty@ya.ru"), new NameCustomer("Иванов И.И.")),
                    new MerchPack(new MerchType(MerchPackType.VeteranPack), new List<FillingItem>()
                    {
                        new FillingItem(FillingItemType.Notepad,
                            (new List<Sku>() {new Sku(123), new Sku(321)}).AsReadOnly()),
                        new FillingItem(FillingItemType.Notepad,
                            (new List<Sku>() {new Sku(666), new Sku(999)}).AsReadOnly()),
                    }.AsReadOnly()),
                    new List<Sku>() {new Sku(66), new Sku(666), new Sku(78), new Sku(98)}.AsReadOnly(),
                    IssueType.Auto);
            });
        }

        [Fact]
        public void EmptySkuListMerchItem()
        {
            Assert.Throws<EmptyCollectionException>(() =>
            {
                var merchItem = new MerchItem(
                    new MerchCustomer(new MailCustomer("qwerty@ya.ru"), new NameCustomer("Иванов И.И.")),
                    new MerchPack(new MerchType(MerchPackType.VeteranPack), new List<FillingItem>()
                    {
                        new FillingItem(FillingItemType.Notepad,
                            (new List<Sku>() {new Sku(123), new Sku(321)}).AsReadOnly()),
                        new FillingItem(FillingItemType.Notepad,
                            (new List<Sku>() {new Sku(666), new Sku(999)}).AsReadOnly()),
                    }.AsReadOnly()),
                    new List<Sku>() { }.AsReadOnly(),
                    IssueType.Auto);
            });
        }
    }
}