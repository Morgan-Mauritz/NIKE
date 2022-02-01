using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NikeClientApp.Views;
using NikeClientApp.ViewModels;

namespace NikeClientApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            
            var navService = DependencyService.Get<INaviService>() as NaviService;
            navService.Navigation = MainPage.Navigation;

           
            navService.RegisterViewMapping(typeof(MainPageViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(MapPageViewModel), typeof(MapPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
