using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;

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

            return new MerchItem(await new MerchItemCustomerRepository(_dbConnectionFactory,_changeTracker).FindByIdAsync(id),
                await new MerchPackRepository(_dbConnectionFactory,_changeTracker).FindByMerchTypeAsync(new MerchType(MerchPackType.VeteranPack),
                    cancellationToken),
                new List<Sku>() {new Sku(123), new Sku(124)},
                IssueType.Auto
            );
        }

        public async Task<List<MerchItem>> FindByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return new List<MerchItem>();
        }
    }
}