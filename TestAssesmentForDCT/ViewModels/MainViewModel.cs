using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestAssesmentForDCT.Infrastructure.Commands;
using TestAssesmentForDCT.Models;
using TestAssesmentForDCT.Services;
using TestAssesmentForDCT.ViewModels.Abstractions;

namespace TestAssesmentForDCT.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private double _frameOpacity;

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
        public double FrameOpacity
        {
            get => _frameOpacity;
            set 
            {
                _frameOpacity = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel() 
        { 
            _coinsPage = new Pages.CoinsPage();
            _exchangesPage = new Pages.ExchangesPage();

            FrameOpacity = 1;

            _currPage = _coinsPage;
        }

        public ICommand CoinsBtnClickCommand 
        { 
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Task.Run(() => SlowOpacity(_coinsPage));
                });
            }
        }

        public ICommand ExchangesBtnClickCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Task.Run(() => SlowOpacity(_exchangesPage));
                });
            }
        }

        private async Task SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() => 
            { 
                for (double i = 1.0; i > 0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }

                CurrentPage = page;

                for (double i = 0; i <= 1.0; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
