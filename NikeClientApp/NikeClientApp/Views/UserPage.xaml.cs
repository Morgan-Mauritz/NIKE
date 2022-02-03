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
        UserPageViewModel ViewModel => BindingContext as UserPageViewModel;


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize ViewModel
          
            await ViewModel?.InitAsync();
        }

        public UserPage()
        {
            InitializeComponent();
            //BackgroundColor = Color.Black;

            BindingContext = new UserPageViewModel(DependencyService.Get<INaviService>());

        }
    }
}