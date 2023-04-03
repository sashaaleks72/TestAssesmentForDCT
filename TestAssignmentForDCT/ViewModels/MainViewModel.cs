using System.Windows.Controls;

namespace TestAssignmentForDCT.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Page _coinsPage;
        private Page _exchangesPage;
        private Page _currPage;

        public Page CurrentPage
        {
            get => _currPage;
            set
            {
                _currPage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel() 
        { 
            _coinsPage = new Pages.CoinsPage();
            _exchangesPage = new Pages.ExchangesPage();

            _currPage = _coinsPage;
        }

        public ICommand CoinsBtnClickCommand => new DelegateCommand((obj) =>
        {
            CurrentPage = _coinsPage;
        });

        public ICommand ExchangesBtnClickCommand => new DelegateCommand((obj) =>
        {
            CurrentPage = _exchangesPage;
        });
    }
}
