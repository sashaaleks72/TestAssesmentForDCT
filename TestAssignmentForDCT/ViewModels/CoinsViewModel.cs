
namespace TestAssignmentForDCT.ViewModels
{
    public class CoinsViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly CoinService _coinService;

        private int _limit = 10;
        private int _page = 0;
        private int? _pagesCount = null;

        private List<Asset> _assets;
        private Asset? _selectedCoin;

        private string _searchWord = string.Empty;

        private string _state = string.Empty;

        public CoinsViewModel()
        {
            _dialogService = new DialogService();
            _coinService = new CoinService();
            _assets = new List<Asset>();
        }

        public string SearchWord
        {
            get => _searchWord;
            set => SetProperty(ref _searchWord, value);
        }

        public List<Asset> Assets
        {
            get => _assets;
            set => SetProperty(ref _assets, value);
        }

        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public Asset? SelectedCoin
        {
            get => _selectedCoin;
            set => SetProperty(ref _selectedCoin, value);
        }

        public int Limit
        {
            get => _limit;
            set => SetProperty(ref _limit, value);
        }

        private async Task<List<Asset>?> RecieveCoinsData(int? limit = null, int? offset = null, string? searchWord = null)
        {
            var list = await Task.Run(async () => await _coinService.GetAssetsListAsync(limit, offset, searchWord));
            return list;
        }

        private async Task SetPagesCountToItsProperty()
        {
            bool isEnd = false;
            int? quantityOfAllCoins = 0;

            for (int i = 0 ;isEnd != true; i+=100)
            {
                var data = await RecieveCoinsData(100, i);

                if (data == null || data.Count == 0) 
                {
                    isEnd = true;
                }

                quantityOfAllCoins += data!.Count;
            }

            if (quantityOfAllCoins != null)
            {
                var roundedValue = Math.Ceiling((decimal)quantityOfAllCoins.Value / _limit);
                _pagesCount = Convert.ToInt32(roundedValue);
            }
        }

        public ICommand PrevBtnCommand => new DelegateCommand(async (obj) =>
        {
            _page--;

            State = "Loading...";
            var coinsList = await RecieveCoinsData(_limit, _page * _limit, _searchWord);
            State = string.Empty;

            if (coinsList != null)
            {
                Assets = coinsList;
            }
        }, (obj) => _page > 0);

        public ICommand ClearBtnCommand => new DelegateCommand((obj) =>
        {
            SearchWord = string.Empty;
        });

        public ICommand FindBtnCommand => new DelegateCommand(async (obj) =>
        {
            State = "Loading...";
            var recievedAssets = await RecieveCoinsData(_limit, _page * _limit, _searchWord);

            if (recievedAssets != null)
            {
                await SetPagesCountToItsProperty();
                State = string.Empty;

                _page = 0;

                Assets = recievedAssets;
            }
        });

        public ICommand NextBtnCommand => new DelegateCommand(async (obj) =>
        {
            State = "Loading...";
            var coinsList = await RecieveCoinsData(_limit, (_page + 1) * _limit, _searchWord);
            State = string.Empty;

            if (coinsList != null && coinsList.Count != 0)
            {
                _page++;
                Assets = coinsList;
            }

        }, (obj) => _page != _pagesCount);

        public ICommand ShowBtnCommand => new DelegateCommand(async (obj) =>
        {
            State = "Loading...";
            var recievedAsset = await Task.Run(async () => await _coinService.GetAssetByIdAsync(SelectedCoin!.Id));

            if (recievedAsset != null)
            {
                var recievedCoinHistory = await Task.Run(async () => await _coinService.GetCoinHistoryListAsync(recievedAsset.Id));
                State = string.Empty;

                if (recievedCoinHistory != null)
                {
                    var detailViewModel = new DetailCoinView
                    {
                        Asset = recievedAsset,
                        Points = recievedCoinHistory
                    };

                    _dialogService.ShowDialog("DetailCoinWindow", detailViewModel);
                }
            }

        }, (obj) => SelectedCoin != null);

        public ICommand RefreshDataCommand => new DelegateCommand(async (obj) =>
        {
            State = "Loading...";
            if (_pagesCount == null)
            {
                await SetPagesCountToItsProperty();
            }

            var coinsList = await RecieveCoinsData(_limit, _page * _limit, _searchWord);
            State = string.Empty;

            if (coinsList != null)
            {
                Assets = coinsList;
            }

        }, (obj) => Limit > 0 && Limit <= 50);

        public ICommand ApplyBtnCommand => new DelegateCommand(async (obj) =>
        {
            bool canBeChanged = Limit > 0 && Limit <= 50 ? true : false;

            if (!canBeChanged)
            {
                MessageBox.Show("Value must be more than 0 and less than 51!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                State = "Loading...";
                await SetPagesCountToItsProperty();
                _page = 0;

                var coinsList = await RecieveCoinsData(_limit, 0, _searchWord);
                State = string.Empty;

                if (coinsList != null)
                {
                    Assets = coinsList;
                }

                MessageBox.Show("New limit applied!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });
    }
}
