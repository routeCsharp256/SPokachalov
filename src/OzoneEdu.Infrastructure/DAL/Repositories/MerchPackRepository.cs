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
            throw new System.NotImplementedException();
        }

        public async Task<MerchPack> FindByMerchTypeAsync(MerchType merchType,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT skus.id, skus.name, skus.item_type_id, skus.clothing_size,
                       stocks.id, stocks.sku_id, stocks.quantity, stocks.minimal_quantity,
                       item_types.id, item_types.name,
                       clothing_sizes.id, clothing_sizes.name
                FROM skus
                INNER JOIN stocks on stocks.sku_id = skus.id
                INNER JOIN item_types on item_types.id = skus.item_type_id
                LEFT JOIN clothing_sizes on clothing_sizes.id = skus.clothing_size
                WHERE skus.id = ANY(@SkuIds);";

            var parameters = new {merchTypeId = merchType.Type.Id};
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchPack = await connection.QueryAsync<
                Models.Sku, MerchPack, MerchPackType, MerchPackItem, MerchPack>
            (commandDefinition,
                (skuModel, pack, packType, packItem) => new MerchPack((int) pack.Id,
                    new MerchType(new MerchPackType(packType.Id, packType.Name)),
                    new List<FillingItem>().AsReadOnly())
            );
            _changeTracker.Track(merchPack.FirstOrDefault());
            return merchPack.FirstOrDefault();
        }
    }
}