using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;
using OzonEdu.MerchApi.Infrastructure.DAL.Models;
using MerchPack = OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate.MerchPack;
using Sku = OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Repositories
{
    public class MerchPackRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
               SELECT merchpack.id as MerchPackId, merchpack.name as MerchPacName,
                      item.id, item.name, sku.id, sku.name, sku.description from merch_pack_items item 
                join merch_packs_items packs on item.id = packs.merchpackitemid
                join merch_packs merchpack on merchpack.id = packs.merchpackid
                join merch_pack_items_skus itemskus on itemskus.merchpackitemid = item.id
                join skus sku on sku.id = itemskus.skuid 
                where merchpackid = @merchTypeId;";

            var parameters = new {merchTypeId = id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchPack = await connection.QueryAsync<
                List<Models.MerchPack>, List<Models.MerchPackItem>,List<Models.Sku>, MerchPack>
            (commandDefinition,
                (merchpacks, 
                    packItems,skus) => new MerchPack((int) merchpacks.FirstOrDefault().MerchPackId,
                    new MerchType(new MerchPackType((int)merchpacks.FirstOrDefault().MerchPackId,
                        merchpacks.FirstOrDefault().MerchPackName)),
                    packItems.Select(
                            i=>new FillingItem((int)i.MerchPackItemId, 
                                new FillingItemType((int)i.MerchPackItemId,i.MerchPackItemName), 
                                skus.Select(
                                        s=>new OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku((long)s.SkuId))
                                    .ToList().AsReadOnly()))
                        .ToList().AsReadOnly()
                )
                    
            );
            _changeTracker.Track(merchPack.FirstOrDefault());
            return merchPack.FirstOrDefault();
        }

        public async Task<MerchPack> FindByMerchTypeAsync(MerchType merchType,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
               SELECT merchpack.id, merchpack.name, item.id, item.name, sku.id, sku.name, sku.description from merch_pack_items item 
                join merch_packs_items packs on item.id = packs.merchpackitemid
                join merch_packs merchpack on merchpack.id = packs.merchpackid
                join merch_pack_items_skus itemskus on itemskus.merchpackitemid = item.id
                join skus sku on sku.id = itemskus.skuid 
                where merchpackid = @merchTypeId;";

            var parameters = new {merchTypeId = merchType.Type.Id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchPack = await connection.QueryAsync<
                List<Models.MerchPack>, List<Models.MerchPackItem>,List<Models.Sku>, MerchPack>
            (commandDefinition,
                (merchpacks,
                    packItems,skus) => new MerchPack((int) merchpacks.FirstOrDefault().MerchPackId,
                    new MerchType(new MerchPackType((int)merchpacks.FirstOrDefault().MerchPackId, merchpacks.FirstOrDefault().MerchPackName)),
                    packItems.Select(
                        i=>new FillingItem((int)i.MerchPackItemId, new FillingItemType((int)i.MerchPackItemId
                                ,i.MerchPackItemName), 
                            skus.Select(
                                s=>new OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects.Sku((long)s.SkuId))
                                .ToList().AsReadOnly()))
                        .ToList().AsReadOnly()
                    )
                    
            );
            _changeTracker.Track(merchPack.FirstOrDefault());
            return merchPack.FirstOrDefault();
        }
    }
}