using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;
using OzonEdu.MerchApi.Infrastructure.DAL.Models;
using IssueType = OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate.IssueType;
using MerchItem = OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate.MerchItem;
using MerchPack = OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate.MerchPack;
using Sku = OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Repositories
{
    public class MerchItemRepository : IMerchItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchItemRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            return itemToCreate;
        }

        public Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchItem> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
            SELECT item.id as MerchItemId, item.orderdate,item.confirmdate,
            customer.id as CustomerId, customer.mail as CustomerMail, customer.mentormail as CustomerMentorMail,customer.mentorname as CustomerMentorName,
            customer.name as CustomerName,
            status.id as StatusId, status.name as StatusName, 
            issuetype.id as IssueTypeId, issuetype.name as IssueTypeName,
            sku.id as SkuId, merchpack.id as MerchPackId, merchpack.name as MerchPackName, items.id as MerchPackItemId,
            items.name as MerchPackItemName,  merchsku.id as MerchItemSku
            from merch_items item 
            join customers customer on customer.id = item.customerid
            join merch_status status on status.id = item.statusid
            join issue_types issuetype on issuetype.id = item.issuetypeid
            join merch_items_skus merchskus on merchskus.merchitemid = item.id
            join skus sku on sku.id = merchskus.skuid
            join merch_packs merchpack on merchpack.id = item.merchpackid
            join merch_packs_items packs on packs.merchpackid = item.merchpackid
            join merch_pack_items items on packs.merchpackitemid = items.id
            join merch_pack_items_skus itemskus on itemskus.merchpackitemid = items.id
            join skus merchsku on merchsku.id = itemskus.skuid 
            where item.id = @Id";

            var parameters = new {Id = id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchItem = await connection.QueryAsync<Models.MerchItem,
                Models.Customer,
                List<Models.Sku>,
                Models.IssueType,
                Models.MerchPack, List<Models.MerchPackItem>, List<Models.Sku>, MerchItem>
            (commandDefinition,
                (merchitem, customer, skus, issuetype,
                        merchpacks, merchpackitems, merchpackitemscus) =>
                    new MerchItem(
                        (int) merchitem.MerchItemId,
                        new MerchCustomer(
                            (int) customer.CustomerId,
                            new MailCustomer(customer.CustomerMail),
                            new NameCustomer(customer.CustomerName),
                            new MailCustomer(customer.CustomerMentorMail),
                            new NameCustomer(customer.CustomerMentorName)
                        ),
                        new MerchPack(
                            (int) merchpacks.MerchPackId,
                            new MerchType(new MerchPackType((int) merchpacks.MerchPackId, merchpacks.MerchPackName)),
                            merchpackitems.Select(
                                    i => new FillingItem((int) i.MerchPackItemId,
                                        new FillingItemType((int) i.MerchPackItemId, i.MerchPackItemName),
                                        merchpackitemscus.Select(
                                                s => new OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku(
                                                    (long) s.SkuId))
                                            .ToList().AsReadOnly()))
                                .ToList().AsReadOnly()
                        ),
                        skus.Select(s => new Sku(s.SkuId)).ToList().AsReadOnly(),
                        new IssueType((int) issuetype.IssueTypeId, issuetype.IssueTypeName)
                    )
            );

            _changeTracker.Track(merchItem.FirstOrDefault());
            return merchItem.FirstOrDefault();
        }

        public async Task<List<MerchItem>> FindByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
           SELECT item.id as MerchItemId, item.orderdate,item.confirmdate,
            customer.id as CustomerId, customer.mail as CustomerMail, customer.mentormail as CustomerMentorMail,customer.mentorname as CustomerMentorName,
                   customer.name as CustomerName,
            issuetype.id as IssueTypeId, issuetype.name as IssueTypeName,
            sku.id as SkuId, merchpack.id as MerchPackId, merchpack.name as MerchPackName, items.id as MerchPackItemId,
            items.name as MerchPackItemName,  merchsku.id as SkuId
            from merch_items item 
            left join customers customer on customer.id = item.customerid
            left join merch_status status on status.id = item.statusid
            left join issue_types issuetype on issuetype.id = item.issuetypeid
            left join merch_items_skus merchskus on merchskus.merchitemid = item.id
            left join skus sku on sku.id = merchskus.skuid
            left join merch_packs merchpack on merchpack.id = item.merchpackid
            left join merch_packs_items packs on packs.merchpackid = item.merchpackid
            left join merch_pack_items items on packs.merchpackitemid = items.id
            left join merch_pack_items_skus itemskus on itemskus.merchpackitemid = items.id
            left join skus merchsku on merchsku.id = itemskus.skuid 
            where  MerchItemId = @MerchItemId";

            var parameters = new {MerchItemId = id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchItem = await connection.QueryAsync<
                Models.MerchItem,
                Models.Customer,
                Models.IssueType,   Models.Sku,
                Models.MerchPack, Models.MerchPackItem, Models.Sku, MerchItem>
            (commandDefinition,
                (merchitem, customer,  issuetype, skus,

                        merchpacks, merchpackitems, merchpackitemscus) =>
                    new MerchItem(
                        (int) merchitem.MerchItemId,
                        new MerchCustomer(
                            (int) customer.CustomerId,
                            new MailCustomer(customer.CustomerMail),
                            new NameCustomer(customer.CustomerName),
                            new MailCustomer(customer.CustomerMentorMail),
                            new NameCustomer(customer.CustomerMentorName)

                        ), 
                        new MerchPack(
                            (int) merchpacks.MerchPackId,
                            new MerchType(new MerchPackType((int) merchpacks.MerchPackId, merchpacks.MerchPackName)),
                            (new List<FillingItem>(){ 
                                     new FillingItem((int) merchpackitems.MerchPackItemId,
                                        new FillingItemType((int) merchpackitems.MerchPackItemId, merchpackitems.MerchPackItemName),
                                        (new List<Sku>(){new Sku(merchpackitemscus.SkuId)}).AsReadOnly()),
                                        
                                    }).AsReadOnly()
                        ),
                        (new List<Sku>(){new Sku(skus.SkuId)}).AsReadOnly(),
                        new IssueType((int) issuetype.IssueTypeId, issuetype.IssueTypeName)
                        )
                        , splitOn:"MerchItemId,CustomerId,IssueTypeId,SkuId,MerchPackId,MerchPackItemId,SkuId"

                );
            foreach (var item in merchItem)
            {
                _changeTracker.Track(item);
            }

            return merchItem.ToList();
        }
    }
}