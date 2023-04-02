namespace TestAssesmentForDCT.Models
{
    public class Exchange
    {
        public int Rank { get; set; }

        public string ExchangeId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public double? PercentTotalVolume { get; set; }

        public double? VolumeUsd { get; set; }

        public int? TradingPairs { get; set; }

        public bool? Socket { get; set; }

        public string ExchangeUrl { get; set; } = string.Empty;
    }
}
