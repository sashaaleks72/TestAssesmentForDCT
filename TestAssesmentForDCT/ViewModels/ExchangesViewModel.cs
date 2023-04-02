using System.Windows.Input;
using TestAssesmentForDCT.Infrastructure.Commands;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;

namespace TestAssesmentForDCT.ViewModels
{
    public class ExchangesViewModel : BaseViewModel
    {
        private List<Exchange> _exchanges;
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
                return new DelegateCommand((obj) =>
                {
                    var list = Task.Run(async () => await _exchangeService.GetExchangesAsync()).Result;

                    if (list != null)
                    {
                        Exchanges = list;
                    }
                });
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
