using NikeClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NikeClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        MapPageViewModel ViewModel => BindingContext as MapPageViewModel;
        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize ViewModel
            ViewModel?.Init();
        }

        public UserPage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;

            BindingContext = new UserPageViewModel(DependencyService.Get<INaviService>());

        }
    }
}