using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchandiseService.Models;
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
            bool isDone = false;
            List<long> skuList = new List<long>();
            foreach (var item in request.Sku)
                skuList.Add(item.Value);

            isDone = await _service.CheckMerch((int) request.UserId, (int) request.TypeId, context.CancellationToken);
            isDone = await _service.CheckAvailableStokMerch(skuList, context.CancellationToken);
            isDone = await _service.ReserveOnStokMerch(skuList.ToList<long>(), context.CancellationToken);

            var merch = await _service.GetMerch(request.UserId,
                skuList,
                request.TypeId,
                (long) IssueType.Manual,
                isDone,
                context.CancellationToken);
            if (merch)
                return new GetMerchItemResponseUnit()
                {
                    ItemId = request.TypeId,
                };
            else
                return new GetMerchItemResponseUnit()
                {
                    ItemId = 0,
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
                    })
                }
            };
        }
    }
}