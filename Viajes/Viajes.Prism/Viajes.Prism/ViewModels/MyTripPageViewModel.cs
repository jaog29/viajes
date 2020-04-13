using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Viajes.Common.Models;
using Viajes.Prism.Helpers;
using Viajes.Prism.Views;

namespace Viajes.Prism.ViewModels
{
    public class MyTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private TripResponse _trip;
        private DelegateCommand _newCostCommand;
        public MyTripPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Details of the Trip";
        }
        public DelegateCommand NewCostCommand => _newCostCommand ?? (_newCostCommand = new DelegateCommand(GoToNewTripDetailPage));

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Trip = parameters.GetValue<TripResponse>("trip");
        }

        public async void GoToNewTripDetailPage()
        {
            await _navigationService.NavigateAsync(nameof(NewTripDetailPage));
        }

    }

}
