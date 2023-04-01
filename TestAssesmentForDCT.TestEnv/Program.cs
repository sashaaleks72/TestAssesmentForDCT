using Newtonsoft.Json;
using TestAssesmentForDCT.TestEnv.Models;

public class Program
{
    private const string _apiUri = "https://api.coincap.io/v2/";

    public static async Task Main(string[] args)
    {
        await GetAssetsList(limit: 10, offset: 0);
    }

    public static async Task GetAssetsList(int? limit, int? offset)
    {
        using var httpClient = new HttpClient { BaseAddress = new Uri(_apiUri) };

        var result = await httpClient.GetAsync($"assets?limit={limit}&offset={offset}");

        if (result.IsSuccessStatusCode) 
        {
            var objToDeserialize = await result.Content.ReadAsStringAsync();

            var assetsResponse = JsonConvert.DeserializeObject<AssetsResponse>(objToDeserialize);

            if (assetsResponse?.Data?.Count != 0)
            {
                foreach (var item in assetsResponse!.Data)
                {
                    Console.WriteLine($"Id - {item.Id}\nName - {item.Name}\nPrice - {item.Price:0.##}\nVwap - {item.Vwap:0.##}\nSupply - {item.Supply:0.##}\nChange - {item.Change:0.##}\nMarketCap - {item.MarketCap:0.##}\nVolume - {item.Volume:0.##}");
                }
            }
        }
    }

}

