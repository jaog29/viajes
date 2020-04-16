using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Common.Services;
using Viajes.Prism.Helpers;

namespace Viajes.Prism.ViewModels
{
    public class TripMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;
        private readonly IApiService _apiService;
        private static TripMasterDetailPageViewModel _instance;

        public TripMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _apiService = apiService;
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }
        public static TripMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_trip",
                    PageName = "MainPage",
                    Title = "New trip"
                },
                new Menu
                {
                    Icon = "ic_details",
                    PageName = "NewTripDetailPage",
                    Title = "Add Details to Trip"
                },
                
                new Menu
                {
                      IsLoginRequired = true,
                    Icon = "ic_history",
                    PageName = "MyTripsPage",
                    Title = Languages.MyTrips
                  
                },

                new Menu
                {
                    Icon = "ic_register",
                    PageName = "RegisterPage",
                    Title = "Register new user"
                },
                new Menu
                {
                    Icon = "ic_modify",
                    PageName = "ModifyUserPage",
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_login",
                    PageName = "LoginPage",
                        Title = Settings.IsLogin ? Languages.Logout : Languages.Login

                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}

