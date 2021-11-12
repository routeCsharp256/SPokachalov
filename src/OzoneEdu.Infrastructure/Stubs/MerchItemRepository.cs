using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Infrastructure.Stubs
{
    public class MerchItemRepository : IMerchItemRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchItem> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {

            return new MerchItem(await new MerchItemCustomerRepository().FindByIdAsync(id),
                await new MerchPackRepository().FindByMerchTypeAsync(new MerchType(MerchPackType.VeteranPack),
                    cancellationToken),
                new List<Sku>() {new Sku(123), new Sku(124)},
                IssueType.Auto
            );
        }

        public async Task<List<MerchItem>> FindByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return new List<MerchItem>()
                {
                    new MerchItem(await new MerchItemCustomerRepository().FindByIdAsync(id,cancellationToken),
                         await new MerchPackRepository().FindByMerchTypeAsync(new MerchType(MerchPackType.VeteranPack),
                            cancellationToken),
                        (new List<Sku>() {new Sku(123), new Sku(124)}).AsReadOnly(),
                        IssueType.Manual
                    )
                };
        }
    }
}