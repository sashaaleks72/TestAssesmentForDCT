using Newtonsoft.Json;

namespace TestAssesmentForDCT.TestEnv.Models
{
    public class Asset
    {
        public string Id { get; set; } = string.Empty;

        public int Rank { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonProperty("priceUsd")]
        public double Price { get; set; }

        [JsonProperty("marketCapUsd")]
        public double MarketCap { get; set; }

        [JsonProperty("vwap24Hr")]
        public double Vwap { get; set; }

        public double Supply { get; set; }

        [JsonProperty("changePercent24Hr")]
        public double Change { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        public double Volume { get; set; }
    }
}
