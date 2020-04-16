using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Viajes.Common.Models;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Viajes.Common.Helpers;
using Viajes.Common.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Viajes.Prism.ViewModels
{
    public class MyTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IFilesHelper _filesHelper;
        private MediaFile _file;
        private ImageSource _image;
        private TripResponse _trip;
        private DelegateCommand _newCostCommand;
        private DelegateCommand _changeImage2Command;
        private List<CostResponse> _costs;
        public MyTripPageViewModel(INavigationService navigationService, IFilesHelper filesHelper)
            : base(navigationService)
        {
            _filesHelper = filesHelper;
            _navigationService = navigationService;
           Image = App.Current.Resources["UrlNoImage"].ToString();
            Title = "Details of the Trip";
        }
        public DelegateCommand NewCostCommand => _newCostCommand ?? (_newCostCommand = new DelegateCommand(GoToNewTripDetailPage));
        public DelegateCommand ChangeImage2Command => _changeImage2Command ?? (_changeImage2Command = new DelegateCommand(ChangeImageAsync));
        public List<CostResponse> Costs
        {
            get => _costs;
            set => SetProperty(ref _costs, value);
        }
        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
          
            Costs = Trip.TripDetails[0].Costs;
        }

        public async void GoToNewTripDetailPage()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "trip", Trip }
            };

            await _navigationService.NavigateAsync(nameof(NewTripDetailPage), parameters);
        
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
    }

}
