using Prism.Commands;
using Prism.Navigation;
using Viajes.Common.Models;
using Viajes.Prism.Views;

namespace Viajes.Prism.ViewModels
{
    public class TripItemViewModel : TripResponse
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectTrip2Command;

        public TripItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
       
        public DelegateCommand SelectTrip2Command => _selectTrip2Command ?? (_selectTrip2Command = new DelegateCommand(SelectTrip2Async));

        private async void SelectTrip2Async()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", this }
            };

            await _navigationService.NavigateAsync(nameof(MyTripPage), parameters);
        }
    }
}
