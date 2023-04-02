using Newtonsoft.Json;
using System.Net.Http;
using TestAssesmentForDCT.Models;

namespace TestAssesmentForDCT.Services
{
    public class ExchangeService
    {
        private const string _apiUri = "https://api.coincap.io/v2/";

        public async Task<List<Exchange>?> GetExchangesAsync()
        {
            BaseResponse<List<Exchange>>? exchangesResponse = null;
            using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

            var result = await httpClient.GetAsync($"exchanges/");

            if (result.IsSuccessStatusCode)
            {
                var objToDeserialize = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(objToDeserialize))
                    exchangesResponse = JsonConvert.DeserializeObject<BaseResponse<List<Exchange>>?>(objToDeserialize);
            }

            var exchangesList = exchangesResponse?.Data;
            return exchangesList;
        }
    }
}
