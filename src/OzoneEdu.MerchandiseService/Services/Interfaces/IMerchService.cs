using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services.Interfaces
{
    public interface IMerchService
    {
        public  Task<int> CreateMerch(long userId, List<long> skuList, long merchTypeId, long issuedType,
            CancellationToken cancelationToken);
        public  Task<bool> SetConfirmStatusMerch(long merchId, bool isDone,
            CancellationToken cancelationToken);
        Task<List<MerchItem>> GetMerchesByUserId(long userId, CancellationToken token);
        public Task<bool> CheckMerch(int userId, int packId, CancellationToken cancelationToken);
        // провкерка на доступность в стоке
        public Task<bool> CheckAvailableStokMerch(List<long> skuList, CancellationToken cancelationToken);
        // зарезервировать  в стоке
        public Task<bool> ReserveOnStokMerch(List<long> skuList, CancellationToken cancelationToken);
    }
}