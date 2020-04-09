    using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Viajes.Common.Helpers;
using Viajes.Common.Services;
using Viajes.Prism.ViewModels;
using Viajes.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Viajes.Prism
{
    public partial class App
    {
       
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {

            SyncfusionLicenseProvider.RegisterLicense("MjM1MDA1QDMxMzgyZTMxMmUzME81YmZCTldtMklhcDZQRkVoMThKQUJ2M3FLYVpzTkd4K0FLa1FJRmt4N289");

            InitializeComponent();

            await NavigationService.NavigateAsync("/TripMasterDetailPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register< IApiService,ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TripMasterDetailPage, TripMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<NewTripDetailPage, NewTripDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<TripHistoryPage, TripHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
        }
    }
}
