using Prism.Navigation;

namespace Viajes.Prism.ViewModels
{
    public class NewTripDetailPageViewModel : ViewModelBase
    {
        public NewTripDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Add New Costs to Trip";
        }
    }
}
