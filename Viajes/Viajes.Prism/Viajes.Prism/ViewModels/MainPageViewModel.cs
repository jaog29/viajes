using Prism.Commands;
using Prism.Navigation;
using Viajes.Common.Helpers;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;

namespace Viajes.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _startTripCommand;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Expense and Travel Control";
        }
        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        private async void StartTripAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(StartTripPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }
    }
}

