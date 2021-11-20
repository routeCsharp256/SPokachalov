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
        public  async Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
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
            SELECT item.id, item.orderdate,item.confirmdate,
            customer.id, customer.mail,customer.mentormail,customer.mentorname,customer.name,
            status.id, status.name, 
            issuetype.id, issuetype.name,
            sku.id,merchpack.id, merchpack.name, items.id, items.name, merchsku.id
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
            var merchItem = await connection.QueryAsync<
                List<Models.MerchItem>, 
                List<Models.Customer>,
                List<Models.Sku>, 
                List<Models.IssueType>,
                List<Models.MerchPack>, List<Models.MerchPackItem>,List<Models.Sku>,MerchItem>
            (commandDefinition,
                (merchitem, customer,skus, issuetype,
                        merchpacks,merchpackitems,merchpackitemscus) => 
                    new MerchItem(
                        (int)merchitem.FirstOrDefault().Id, 
                        new MerchCustomer(
                            (int)customer.FirstOrDefault().Id,
                            new MailCustomer(customer.FirstOrDefault().Mail),
                            new NameCustomer(customer.FirstOrDefault().Name),
                            new MailCustomer(customer.FirstOrDefault().MentorMail),
                            new NameCustomer(customer.FirstOrDefault().MentorName)
                            ),
                        new MerchPack(
                            (int) merchpacks.FirstOrDefault().Id,
                            new MerchType(new MerchPackType((int)merchpacks.FirstOrDefault().Id, merchpacks.FirstOrDefault().Name)),
                            merchpackitems.Select(
                                    i=>new FillingItem((int)i.Id, new FillingItemType((int)i.Id,i.Name), 
                                        merchpackitemscus.Select(
                                                s=>new OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku((long)s.Id))
                                            .ToList().AsReadOnly()))
                                .ToList().AsReadOnly()
                        ),
                        skus.Select(s=> new Sku(s.Id)).ToList().AsReadOnly(),
                        new IssueType((int)issuetype.FirstOrDefault().Id,issuetype.FirstOrDefault().Name)
                        )
            );
            
            _changeTracker.Track(merchItem.FirstOrDefault());
            return merchItem.FirstOrDefault();
        }

        public async Task<List<MerchItem>> FindByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
           const string sql = @"
            SELECT item.id, item.orderdate,item.confirmdate,
            customer.id, customer.mail,customer.mentormail,customer.mentorname,customer.name,
            status.id, status.name, 
            issuetype.id, issuetype.name,
            sku.id,merchpack.id, merchpack.name, items.id, items.name, merchsku.id
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
            where  customer.id = @Id";

            var parameters = new {Id = id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchItem = await connection.QueryAsync<
                Models.MerchItem, 
                Models.Customer,
               List<Models.Sku>, 
                Models.IssueType,
                Models.MerchPack, List<Models.MerchPackItem>,List<Models.Sku>,MerchItem>
            (commandDefinition,
                (merchitem, customer,skus, issuetype,
                        merchpacks,merchpackitems,merchpackitemscus) => 
                    new MerchItem(
                        (int)merchitem.Id, 
                        new MerchCustomer(
                            (int)customer.Id,
                            new MailCustomer(customer.Mail),
                            new NameCustomer(customer.Name),
                            new MailCustomer(customer.MentorMail),
                            new NameCustomer(customer.MentorName)
                            ),
                        new MerchPack(
                            (int) merchpacks.Id,
                            new MerchType(new MerchPackType((int)merchpacks.Id, merchpacks.Name)),
                            merchpackitems.Select(
                                    i=>new FillingItem((int)i.Id, new FillingItemType((int)i.Id,i.Name), 
                                        merchpackitemscus.Select(
                                                s=>new OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku((long)s.Id))
                                            .ToList().AsReadOnly()))
                                .ToList().AsReadOnly()
                        ),
                        skus.Select(s=> new Sku(s.Id)).ToList().AsReadOnly(),
                        new IssueType((int)issuetype.Id,issuetype.Name)
                        )
            );
            foreach (var item in merchItem)
            {
                _changeTracker.Track(item);
            }
            return merchItem.ToList();
        }
    }
}