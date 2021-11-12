using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Infrastructure.Stubs
{
    public class MerchPackRepository : IMerchPackRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var type = MerchPackType.GetAll<MerchPackType>().FirstOrDefault();
            var items = new List<FillingItem>();
            items.Add(new FillingItem(FillingItemType.Pen, (new List<Sku>()
            {
                new Sku(123), new Sku(66), new Sku(12)
            }).AsReadOnly()));
            items.Add(new FillingItem(FillingItemType.Notepad, (new List<Sku>()
            {
                new Sku(78), new Sku(6645), new Sku(142)
            }).AsReadOnly()));
            return new MerchPack(type: new MerchType(type), items);
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