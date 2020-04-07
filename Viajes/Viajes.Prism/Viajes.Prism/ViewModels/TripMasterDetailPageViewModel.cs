using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Viajes.Common.Helpers;
using Viajes.Common.Models;
using Viajes.Prism.Helpers;

namespace Viajes.Prism.ViewModels
{
    public class TripMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public TripMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoadUser();

            _navigationService = navigationService;
            LoadMenus();
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
                    Icon = "ic_history",
                    PageName = "TripHistoryPage",
                    Title = "See trip history"
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

