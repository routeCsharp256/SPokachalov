using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;

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
            var type = MerchPackType.GetAll<MerchPackType>().FirstOrDefault(x => x.Equals(merchType.Type));
            var items = new List<FillingItem>();
            items.Add(new FillingItem(FillingItemType.Pen, (new List<Sku>()
            {
                new Sku(123), new Sku(124), new Sku(125)
            }).AsReadOnly()));
            items.Add(new FillingItem(FillingItemType.Notepad, (new List<Sku>()
            {
                new Sku(124), new Sku(123), new Sku(125)
            }).AsReadOnly()));
            return new MerchPack(type: new MerchType(type), items.AsReadOnly());
        }
    }
}