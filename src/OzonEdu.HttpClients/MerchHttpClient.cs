using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.HttpClients.Interfaces;
using OzonEdu.MerchApi.HttpModels;

namespace OzonEdu.MerchApi.HttpClients
{
    public class MerchHttpClient : IMerchHttpClient
    {
        private readonly HttpClient _httpClient;
        
        public MerchHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async  Task<MerchItemResponse> V1GetMerch(long userId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/{userId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<MerchItemResponse>(body);
        }

        public async Task<List<MerchItemResponse>> V1GetMerchesByUserId(long userId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/info/{userId}", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<MerchItemResponse>>(body);
        }
    }
}