using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Infrastructure.Stubs
{
    public class MerchItemCustomerRepository : IMerchItemCustomerRepository
    {
        public IUnitOfWork UnitOfWork { get; }
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