using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;
using Xamarin.Forms;

namespace Viajes.Prism.ViewModels
{


    public class StartTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private MediaFile _file;
        private ImageSource _image;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _cancelTripCommand;
        private bool _isSecondButtonVisible;
        private bool _isRunning;
        private bool _isEnabled;
        

        public StartTripPageViewModel(INavigationService navigationService, IRegexHelper regexHelper,
                                       IApiService apiService,
                                        IFilesHelper filesHelper)
                                         : base(navigationService)
        {
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = Languages.StartTrip;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;

        }
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        public DelegateCommand CancelTripCommand => _cancelTripCommand ?? (_cancelTripCommand = new DelegateCommand(CancelAsync));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

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
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
  
        public string Category { get; set; }
        public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
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


        private async void CancelAsync()
        {
            await _navigationService.NavigateAsync(nameof(MainPage));
        }
        private async void SaveAsync()
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
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);
                return;
            }
            byte[] imageArray = null;
            if (_file != null)
            {

                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            TripRequest tripRequest = new TripRequest
            {
                UserId = new Guid(user.Id),
                StartDateTrip = StartDate,
                EndDateTrip = EndDate,
                DestinyCity = Destiny,
                Origin = Origin,
                Description = Description,
                PicturePath = imageArray,
                Value = Value,
                Category = Category,
                CreatedDate = DateTime.Now
            };

            Response response = await _apiService.NewTripAsync(url, "/api", "/trips", tripRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.successful, Languages.Accept);
            await _navigationService.GoBackAsync();
        }
        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
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
