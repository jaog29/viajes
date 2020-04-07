using Prism.Commands;
using Prism.Navigation;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;

namespace Viajes.Prism.ViewModels
{

    public class TripHistoryPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TripResponse _trip;
        private DelegateCommand _CheckDestinyCommand;
        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public TripHistoryPageViewModel(
                INavigationService navigationService,
                IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.TripHistory;

        }

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public int id { get; set; }

        public DelegateCommand CheckDestinyCommand => _CheckDestinyCommand ?? (_CheckDestinyCommand = new DelegateCommand(CheckDestinyAsync));

        private async void CheckDestinyAsync()
        {

            /*if (string.IsNullOrEmpty(id))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a ID.",
                    "Accept");
                return;
            }*/



            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetTripAsync(id, url, "api", "/trip");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Trip = (TripResponse)response.Result;
        }
    }
}


