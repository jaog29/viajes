using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Viajes.Common.Enums;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;

namespace Viajes.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        private DelegateCommand _changePasswordCommand;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private UserResponse _user;
        private DelegateCommand _saveCommand;
        private readonly INavigationService _navigationService;
        public ModifyUserPageViewModel(INavigationService navigationService,IApiService apiService)
            : base(navigationService)
        {
            Title = Languages.ModifyUser;
            IsEnabled = true;
            User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            _navigationService = navigationService;
            _apiService = apiService;
    
        }
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
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
        private async void ChangePasswordAsync()
        {
            await _navigationService.NavigateAsync(nameof(ChangePasswordPage));
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
            var userRequest = new UserRequest
            {
                
                Document = User.Document,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = "123456", // It doesn't matter what is sent here. It is only for the model to be valid
                Phone = User.PhoneNumber,
                UserTypeId = User.UserType == UserType.User ? 1 : 2,
                CultureInfo = Languages.Culture
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(url, "/api", "/Account", userRequest, "bearer", token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Settings.User = JsonConvert.SerializeObject(User);
            TripMasterDetailPageViewModel.GetInstance().ReloadUser();
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.UserUpdated, Languages.Accept);
        }

    

    private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumentError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

           

            if (string.IsNullOrEmpty(User.PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            return true;
        }

    }
}
