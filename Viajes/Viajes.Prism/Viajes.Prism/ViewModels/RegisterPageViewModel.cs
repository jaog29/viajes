using Prism.Navigation;

namespace Viajes.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public RegisterPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Register New User";
        }
    }
}
