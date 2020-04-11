using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Prism.Helpers;

namespace Viajes.Prism.ViewModels
{


        public class StartTripPageViewModel : ViewModelBase
        {
            private readonly INavigationService _navigationService;
            //private string _source;
            //private string _buttonLabel;
            private bool _isSecondButtonVisible;
            private bool _isRunning;
            private bool _isEnabled;
            //private DelegateCommand _getAddressCommand;
            private DelegateCommand _startTripCommand;

            public StartTripPageViewModel(INavigationService navigationService)
                : base(navigationService)
            {
                _navigationService = navigationService;
                //_geolocatorService = geolocatorService;
                Title = Languages.StartTrip;
                //ButtonLabel = Languages.StartTrip;
                //LoadSourceAsync();
            }

            //public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));

            public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

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

            private async void StartTripAsync()
            {
                bool isValid = await ValidateDataAsync();
                if (!isValid)
                {
                    return;
                }
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
