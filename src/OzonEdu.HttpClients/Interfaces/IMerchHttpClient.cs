using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.HttpModels;

namespace OzonEdu.MerchApi.HttpClients.Interfaces
{
    public interface IMerchHttpClient
    {
        Task<MerchItemResponse> V1GetMerch(long userId, CancellationToken token);
        Task<List<MerchItemResponse>> V1GetMerchesByUserId(long userId, CancellationToken token);
    }
}