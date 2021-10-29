using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchandiseService.Services.Interfaces;
using OzonEdu.MerchApi.Grpc;

namespace OzoneEdu.MerchandiseService.GrpcServices
{
    public sealed class MerchApiGrpcService : MerchApiGrpc.MerchApiGrpcBase
    {
        private readonly IMerchService _service;

        public MerchApiGrpcService(IMerchService service)
        {
            _service = service;
        }

        public override async Task<GetMerchItemResponseUnit> GetMerch(GetMerchItemsRequest request,
            ServerCallContext context)
        {
            var merch = await _service.GetMerch(request.UserId, context.CancellationToken);
            return new GetMerchItemResponseUnit
            {
                ItemId = merch.ItemId,
                ItemName = merch.ItemName
            };
        }

        public override async Task<GetMerchItemsResponse> GetMerchInfoByUserId(GetMerchItemsRequest request,
            ServerCallContext context)
        {
            var listMerch = await _service.GetMerchesByUserId(request.UserId, context.CancellationToken);
            return new GetMerchItemsResponse
            {
                MerchItems =
                {
                    listMerch.Select(x => new GetMerchItemResponseUnit()
                    {
                        ItemId = x.ItemId,
                        ItemName = x.ItemName
                    })
                }
            };
        }
    }
}