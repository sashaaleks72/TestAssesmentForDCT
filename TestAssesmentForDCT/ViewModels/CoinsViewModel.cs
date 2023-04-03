using System.Windows;
using System.Windows.Input;
using TestAssesmentForDCT.Infrastructure.Commands;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;
using TestAssesmentForDCT.Views.Windows;

namespace TestAssesmentForDCT.ViewModels
{
    public class CoinsViewModel : BaseViewModel
    {
        private readonly CoinService _coinService;

        private int _limit = 10;
        private int _page = 0;
        private int? _pagesCount = null;

        private Asset? _selectedCoin;

        public CoinsViewModel()
        {
            _coinService = new CoinService();
            _assets = new List<Asset>();
        }

        public Asset? SelectedCoin
        {
            get { return _selectedCoin; }
            set 
            { 
                _selectedCoin = value;
                OnPropertyChanged();
            }
        }

        public int Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                OnPropertyChanged();
            }
        }

        private List<Asset>? RecieveCoinsData(int? limit = null, int? offset = null)
        {
            var list = Task.Run(async () => await _coinService.GetAssetsListAsync(limit, offset)).Result;
            return list;
        }

        private void SetPagesCountToItsProperty()
        {
            bool isEnd = false;
            int? quantityOfAllCoins = 0;

            for (int i = 0 ;isEnd != true; i+=100)
            {
                var data = RecieveCoinsData(100, i);

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

        public ICommand PrevBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _page--;
                    var coinsList = RecieveCoinsData(_limit, _page * _limit);

                    if (coinsList != null)
                    {
                        Assets = coinsList;
                    }
                }, (obj) => _page > 0);
            }
        }

        public ICommand FindBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) => { });
            }
        }

        public ICommand NextBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var coinsList = RecieveCoinsData(_limit, (_page + 1) * _limit);

                    if (coinsList != null && coinsList.Count != 0)
                    {
                        _page++;
                        Assets = coinsList;
                    }

                }, (obj) => _page < _pagesCount);
            }
        }

        public ICommand ShowBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var recievedAsset = Task.Run(async () => await _coinService.GetAssetByIdAsync(SelectedCoin!.Id)).Result;

                    if (recievedAsset != null)
                    {
                        //MessageBox.Show($"Id: {recievedAsset.Id}\nName: {recievedAsset.Name}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        var detailWindow = new DetailCoinWindow();
                        detailWindow.ShowDialog();
                    }
                    
                }, (obj) => SelectedCoin != null);
            }
        }

        public ICommand RefreshDataCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (_pagesCount == null)
                        SetPagesCountToItsProperty();

                    var coinsList = RecieveCoinsData(_limit, _page * _limit);

                    if (coinsList != null)
                    {
                        Assets = coinsList;
                    }

                }, (obj) => Limit > 0 && Limit <= 50);
            }
        }

        public ICommand ApplyBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    bool canBeChanged = Limit > 0 && Limit <= 50 ? true : false;

                    if (!canBeChanged)
                    {
                        MessageBox.Show("Value must be more than 0 and less than 51!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        SetPagesCountToItsProperty();

                        var coinsList = RecieveCoinsData(_limit, 0);

                        if (coinsList != null)
                        {
                            Assets = coinsList;
                        }

                        MessageBox.Show("New limit applied!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
            }
        }

        private List<Asset> _assets;
        public List<Asset> Assets
        {
            get
            {
                return _assets;
            }
            set
            {
                _assets = value;
                OnPropertyChanged();
            }
        }
    }
}
