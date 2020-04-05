using Prism.Navigation;

namespace Viajes.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Control Trips and Costs";
        }
    }
}
