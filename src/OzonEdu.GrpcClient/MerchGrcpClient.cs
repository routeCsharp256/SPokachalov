using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.MerchApi.Grpc;
using OzonEdu.MerchApi.GrpcClient.Interfaces;
using OzonEdu.MerchApi.HttpModels;

namespace OzonEdu.MerchApi.GrpcClient
{
    public class MerchGrcpClient : IMerchGrcpClient
    {
        private MerchApiGrpc.MerchApiGrpcClient _client;
        private GrpcChannel _channel;

        public MerchGrcpClient(GrpcChannel channel)
        {
            _channel = channel;
            _client = new MerchApiGrpc.MerchApiGrpcClient(_channel);
        }

        public async Task<MerchItemResponse> V1GetMerch(long userId, CancellationToken token)
        {
            throw new RpcException(Status.DefaultSuccess);
            var grcpMerch = await _client.GetMerchAsync(new GetMerchItemsRequest() {UserId = userId});
            return new MerchItemResponse()
            {
                ItemId = grcpMerch.ItemId, ItemName = grcpMerch.ItemName
            };
        }

        public async Task<List<MerchItemResponse>> V1GetMerchesByUserId(long userId, CancellationToken token)
        {
            List<MerchItemResponse> merchList = new List<MerchItemResponse>();
            var grcpMerchList = await _client.GetMerchInfoByUserIdAsync(new GetMerchItemsRequest() {UserId = userId});

            foreach (var getMerchItemResponseUnit in grcpMerchList.MerchItems)
                merchList.Add(new MerchItemResponse()
                {
                    ItemId = getMerchItemResponseUnit.ItemId,
                    ItemName = getMerchItemResponseUnit.ItemName
                });
            return merchList;
        }
    }
}