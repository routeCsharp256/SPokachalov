using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services.Interfaces
{
    public interface IMerchService
    {
        Task<MerchItem> GetMerch(long userId, CancellationToken token);
        Task<List<MerchItem>> GetMerchesByUserId(long userId, CancellationToken _);
    }
}