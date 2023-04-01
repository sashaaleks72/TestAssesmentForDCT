using Newtonsoft.Json;
using System.Net.Http;
using TestAssesmentForDCT.Models;

namespace TestAssesmentForDCT.Services
{
    public class DataService
    {
        private const string _apiUri = "https://api.coincap.io/v2/";

        public async Task<List<Asset>?> GetAssetsListAsync(int? limit, int? offset)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

            var result = await httpClient.GetAsync($"assets?limit={limit}&offset={offset}");

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var objToDeserialize = await result.Content.ReadAsStringAsync();
            var assetsResponse = JsonConvert.DeserializeObject<AssetsResponse>(objToDeserialize);

            var assetsList = assetsResponse?.Data;

            return assetsList;
        }
    }
}
