
namespace TestAssesmentForDCT.ViewModels
{
    public class ExchangesViewModel : BaseViewModel
    {
        private List<Exchange> _exchanges;
        private string _state = string.Empty;
        private readonly ExchangeService _exchangeService;

        public ExchangesViewModel()
        {
            _exchangeService = new ExchangeService();
            _exchanges = new List<Exchange>();
        }

        public ICommand RefreshDataCommand
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    State = "Loading...";
                    var list = await Task.Run(async () => await _exchangeService.GetExchangesAsync());
                    State = string.Empty;

                    if (list != null)
                    {
                        Exchanges = list;
                    }
                });
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

        public List<Exchange> Exchanges
        {
            get
            {
                return _exchanges;
            }
            set
            {
                _exchanges = value;
                OnPropertyChanged();
            }
        }
    }
}
