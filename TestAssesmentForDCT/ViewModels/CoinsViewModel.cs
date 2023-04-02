using System.Windows;
using System.Windows.Input;
using TestAssesmentForDCT.Infrastructure.Commands;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;

namespace TestAssesmentForDCT.ViewModels
{
    public class CoinsViewModel : BaseViewModel
    {
        private readonly CoinService _coinService;
        
        private int _limit = 10;

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

        public ICommand ShowBtnCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var recievedAsset = Task.Run(async () => await _coinService.GetAssetByIdAsync(SelectedCoin!.Id)).Result;

                    if (recievedAsset != null)
                    {
                        MessageBox.Show($"Id: {recievedAsset.Id}\nName: {recievedAsset.Name}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    var list = Task.Run(async () => await _coinService.GetAssetsListAsync(_limit, 0)).Result;

                    if (list != null)
                    {
                        Assets = list;
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
                        var list = Task.Run(async () => await _coinService.GetAssetsListAsync(_limit, 0)).Result;

                        if (list != null)
                        {
                            Assets = list;
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
