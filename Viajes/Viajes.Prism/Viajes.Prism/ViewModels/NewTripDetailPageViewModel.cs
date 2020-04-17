using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;

namespace Viajes.Prism.ViewModels
{


    public class NewTripDetailPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TripResponse _trip;
        private DelegateCommand _saveCost;
        public NewTripDetailPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {


            _navigationService = navigationService;
            _apiService = apiService;
            _trip = new TripResponse();
            Title = "Add New Costs to Trip";
            IsEnabled = true;
            IsRunning = false;

        }

        public DelegateCommand SaveCost => _saveCost ?? (_saveCost = new DelegateCommand(SaveNewCost));


        public int Value { get; set; }

        public string Category { get; set; }

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
        }
        private async void SaveNewCost()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);



            var TripRequest = new TripRequest
            {
              
            DestinyCity=Trip.DestinyCity,
           
                Value = Value,
                Category = Category,
                CreatedDate = DateTime.Now
                          };

            Response response = await _apiService.NewTripAsync(url, "/api", "/trips/AddCost", TripRequest, "bearer", token.Token);

            //var response = await _apiService.AddNewCost(url, "/api", "/trips/AddCost", "bearer", token.Token, CostTripRequest);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, "New added Cost", Languages.Accept);
                 await _navigationService.GoBackAsync();
          

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (Value==0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Enter the value of the expense", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Category))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Enter the expense category", Languages.Accept);
                return false;
            }

            return true;
        }
    }

}
