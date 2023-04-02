using Newtonsoft.Json;
using System.Net.Http;
using TestAssesmentForDCT.Models;

namespace TestAssesmentForDCT.Services
{
    public class CoinService
    {
        private const string _apiUri = "https://api.coincap.io/v2/";

        public async Task<List<Asset>?> GetAssetsListAsync(int? limit, int? offset)
        {
            BaseResponse<List<Asset>>? assetsResponse = null;
            using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

            var result = await httpClient.GetAsync($"assets?limit={limit}&offset={offset}");

            if (result.IsSuccessStatusCode)
            {
                var objToDeserialize = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(objToDeserialize))
                    assetsResponse = JsonConvert.DeserializeObject<BaseResponse<List<Asset>>>(objToDeserialize);
            }

            var assetsList = assetsResponse?.Data;
            return assetsList;
        }

        public async Task<Asset?> GetAssetByIdAsync(string id = "bitcoin")
        {
            BaseResponse<Asset>? asset = null;
            using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

            var result = await httpClient.GetAsync($"assets/{id}");

            if (result.IsSuccessStatusCode)
            {
                var objToDeserialize = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(objToDeserialize))
                    asset = JsonConvert.DeserializeObject<BaseResponse<Asset>?>(objToDeserialize);
            }

            return asset?.Data;
        }
    }
}
