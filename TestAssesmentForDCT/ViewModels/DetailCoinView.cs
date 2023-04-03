using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;

namespace TestAssesmentForDCT.ViewModels
{
    public class DetailCoinView : BaseViewModel
    {
        private readonly CoinService _coinService;
        private Asset _asset;

        public DetailCoinView() 
        {
            _coinService = new CoinService();
            _asset = new Asset
            {
                Rank = 1234,
                Change = 0.44,
                MarketCap = 24343,
                Symbol = "BTC",
                Name = "Bitcoin",
                Volume = 2454,
                Price = 28654.45,
                Supply = 454545,
                MaxSupply = 5445599
            };
        }

        public Asset Asset 
        {
            get => _asset;
            set
            {
                _asset = value;
                OnPropertyChanged();
            } 
        }
    }
}
