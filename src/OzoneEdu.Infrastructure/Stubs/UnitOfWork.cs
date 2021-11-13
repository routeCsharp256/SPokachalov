using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Infrastructure.Stubs
{
    public class UnitOfWork:IUnitOfWork
    
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return true;
        }
    }
}