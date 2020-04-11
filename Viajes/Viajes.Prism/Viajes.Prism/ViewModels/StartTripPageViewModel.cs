using FFImageLoading.Work;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Viajes.Prism.ViewModels
{


        public class StartTripPageViewModel : ViewModelBase
        {
            private MediaFile _file;
            private DelegateCommand _changeImageCommand;
            private readonly INavigationService _navigationService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        //private ImageSource _image;
        //private string _source;
        //private string _buttonLabel;
        private bool _isSecondButtonVisible;
            private bool _isRunning;
            private bool _isEnabled;
            //private DelegateCommand _getAddressCommand;
            private DelegateCommand _cancelTripCommand;
            private TripResponse _tripResponse;
            private TripDetailResponse _tripDetailResponse;
            private CostResponse _costResponse;
            private UserResponse _user;
             private TokenResponse _token;
        private DelegateCommand _saveCommand;
        public StartTripPageViewModel(INavigationService navigationService, IRegexHelper regexHelper,
                                       IApiService apiService)
                                           //IFilesHelper filesHelper)
                                         : base(navigationService)
            {
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            //_filesHelper = filesHelper;
            //User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            //_geolocatorService = geolocatorService;
            Title = Languages.StartTrip;
            //ButtonLabel = Languages.StartTrip;
            //LoadSourceAsync();
            //Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;

        }

        //public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));
        //public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        //public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        public DelegateCommand CancelTripCommand => _cancelTripCommand ?? (_cancelTripCommand = new DelegateCommand(CancelAsync));

        [DataType(DataType.Date)]
            [Display(Name = "Start Date Trip")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
            public DateTime StartDate { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "End Date Trip")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]

            public DateTime EndDate { get; set; }

            public string Destiny { get; set; }
            public string Origin { get; set; }

            public string Description { get; set; }
            public string PicturePath { get; set; }
            public float Value { get; set; }
        /*public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }*/
        public string Category { get; set; }
            public bool IsSecondButtonVisible
            {
                get => _isSecondButtonVisible;
                set => SetProperty(ref _isSecondButtonVisible, value);
            }

            /*public string ButtonLabel
            {
                get => _source;
                set => SetProperty(ref _source, value);
            }*/

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

        /*public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }*/

        /*   private async void LoadSourceAsync()
           {
               await _geolocatorService.GetLocationAsync();
               if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
               {
                   Position position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
                   Geocoder geoCoder = new Geocoder();
                   IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(position);
                   List<string> addresses = new List<string>(sources);

                   if (addresses.Count > 0)
                   {
                       Source = addresses[0];
                   }
               }
           }*/
        private async void CancelAsync()
        {
            await _navigationService.NavigateAsync(nameof(MainPage));
        }
            private async void SaveAsync()
            {
                var isValid = await ValidateDataAsync();
                if (!isValid)
                {
                    return;
                }
            IsRunning = true;
            IsEnabled = false;
            /*byte[] imageArray = null;
           if (_file != null)
           {
               imageArray = _filesHelper.ReadFully(_file.GetStream());
           }*/

           

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            TripRequest tripRequest = new TripRequest
            {
              
                Value=Value,
                Category=Category,
                PicturePath=PicturePath,
                Origin=Origin,
                Description=Description,
                DestinyCity=Destiny,
                EndDateTrip =EndDate,
                StartDateTrip =StartDate,
                UserId = new Guid(user.Id)
            };

            Response response = await _apiService.NewTripAsync(url, "/api", "/trips", tripRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            _tripResponse = (TripResponse)response.Result;
            _tripDetailResponse = (TripDetailResponse)response.Result;
            _costResponse = (CostResponse)response.Result;
            //IsSecondButtonVisible = true;
            //ButtonLabel = Languages.EndTrip;
            //StartTripPage.GetInstance().AddPin(_position, Source, Languages.StartTrip, PinType.Place);
            IsRunning = false;
            IsEnabled = true;
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.successful, Languages.Accept);
          
        }


        private async Task<bool> ValidateDataAsync()
            {
                if (string.IsNullOrEmpty(Convert.ToString(StartDate)))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.StartDate, Languages.Accept);
                    return false;
                }

                if (string.IsNullOrEmpty(Convert.ToString(EndDate)))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EndDate, Languages.Accept);
                    return false;
                }

                if (string.IsNullOrEmpty(Destiny))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Destiny, Languages.Accept);
                    return false;
                }

                if (string.IsNullOrEmpty(Origin))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Origin, Languages.Accept);
                    return false;
                }

                if (string.IsNullOrEmpty(Description))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Description, Languages.Accept);
                    return false;
                }
                if (string.IsNullOrEmpty(Convert.ToString(Value)))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Value, Languages.Accept);
                    return false;
                }
                if (string.IsNullOrEmpty(Convert.ToString(Category)))
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Category, Languages.Accept);
                    return false;
                }
                return true;
            }
        }
    }
