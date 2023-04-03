
namespace TestAssesmentForDCT.ViewModels
{
    public class DetailCoinView : BaseViewModel
    {
        private readonly CoinService _coinService;
        private Asset _asset;
        private string _state = string.Empty;
        private IList<CoinHistory> _points;

        public ICommand RefreshDataCommand 
        { 
            get
            {
                return new DelegateCommand(async (obj) =>
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
            } 
        }

        public DetailCoinView() 
        {
            _coinService = new CoinService();
            _points = new List<CoinHistory>();
            _asset = new Asset();
        }

        public IList<CoinHistory> Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPropertyChanged();
            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
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
