using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Repositories
{
    public class MerchItemCustomerRepository : IMerchItemCustomerRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchItemCustomerRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker; 
        }
        public Task<IMerchItemCustomerRepository> CreateAsync(IMerchItemCustomerRepository itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IMerchItemCustomerRepository> UpdateAsync(IMerchItemCustomerRepository itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchCustomer> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var customer = new MerchCustomer(new MailCustomer("goolgemoogle@ggg.com")
                , new NameCustomer("Пиндриков А.А"));
            var enoterCustomer = new MerchCustomer(new MailCustomer("goolgemoogle@ggg.com")
                , new NameCustomer("Егоров Л.В"));
            if (id > 100)
                 return customer;
            else 
                return enoterCustomer;
        }

        public Task<MerchCustomer> FindByMailAsync(MailCustomer mailCustomer, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}