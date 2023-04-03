
namespace TestAssesmentForDCT.ViewModels
{
    public class DetailCoinView : BaseViewModel
    {
        private readonly CoinService _coinService;
        private Asset _asset;
        private string _state = string.Empty;
        private IList<CoinHistory> _points;

        public DetailCoinView() 
        {
            _coinService = new CoinService();
            _points = new List<CoinHistory>();
            _asset = new Asset();
        }

        public ICommand RefreshDataCommand => new DelegateCommand(async (obj) =>
        {
            State = "Loading...";
            var recievedAsset = await Task.Run(async () => await _coinService.GetAssetByIdAsync(Asset.Id));

            if (recievedAsset != null)
            {
                var recievedCoinHistory = await Task.Run(async () => await _coinService.GetCoinHistoryListAsync(recievedAsset.Id));
                State = string.Empty;

                if (recievedCoinHistory != null)
                {
                    Asset = recievedAsset;
                    Points = recievedCoinHistory;
                }
            }
        });

        public IList<CoinHistory> Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public Asset Asset 
        {
            get => _asset;
            set => SetProperty(ref _asset, value);
        }
    }
}
