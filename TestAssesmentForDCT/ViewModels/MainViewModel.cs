using System.Collections.ObjectModel;
using System.Windows.Input;
using TestAssesmentForDCT.Infrastructure.Commands;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;

namespace TestAssesmentForDCT.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private int _limit = 10;

        public MainViewModel() 
        {
            _dataService = new DataService();
            Assets = new List<Asset>();
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

        public ICommand RefreshDataCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var list = Task.Run(async () => await _dataService.GetAssetsListAsync(_limit, 0)).Result;

                    if (list != null)
                    {
                        Assets = list;
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
