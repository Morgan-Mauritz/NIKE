using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NikeClientApp.Views;
namespace NikeClientApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MapPage();
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
