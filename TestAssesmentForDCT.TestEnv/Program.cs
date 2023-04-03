using Newtonsoft.Json;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.TestEnv.Models;

public class Program
{
    private const string _apiUri = "https://api.coincap.io/v2/";

    public static async Task Main(string[] args)
    {
        //await GetAssetsListAsync(limit: 10, offset: 0);
        //await GetAssetByIdAsync("bitcoin");
        //await GetExchangesAsync();
        await GetCoinHistoryListAsync("bitcoin");
    }

    public static async Task<List<CoinHistory>?> GetCoinHistoryListAsync(string coinId)
    {
        BaseResponse<List<CoinHistory>>? coinHistoryResponse = null;
        using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

        var result = await httpClient.GetAsync($"assets/{coinId}/history?interval=h1");

        if (result.IsSuccessStatusCode)
        {
            var objToDeserialize = await result.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(objToDeserialize))
                coinHistoryResponse = JsonConvert.DeserializeObject<BaseResponse<List<CoinHistory>>>(objToDeserialize);
        }

        var coinHistoryList = coinHistoryResponse?.Data;
        return coinHistoryList;
    }

    public static async Task<List<Asset>?> GetAssetsListAsync(int? limit, int? offset)
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

    public static async Task<Asset?> GetAssetByIdAsync(string id = "bitcoin")
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

    public static async Task<List<Exchange>?> GetExchangesAsync()
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

