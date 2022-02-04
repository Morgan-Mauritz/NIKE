using NikeClientApp.Models;
using NikeClientApp.Services;
using NikeClientApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using RestSharp;
using NikeClientApp.ViewModels;

namespace NikeClientApp
{
    public partial class MainPage : ContentPage
    {
        HttpService<User> userClient = new HttpService<User>();
        HttpService<POI> poiClient = new HttpService<POI>();

        MainPageViewModel ViewModel => BindingContext as MainPageViewModel;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(DependencyService.Get<INaviService>()); 
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize ViewModel
            ViewModel?.Init();
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void ToMaps_Clicked(object sender, EventArgs e)
        {
           var ans = await DisplayAlert("Varning", "För att kunna lägga till sevärdheter, kommentera och betygsätta, \nmåste du först registrera dig.", "OK","Avbryt");
            if (ans == true)
            {
                await Navigation.PushAsync(new MapPage());
            }
           

            
        }
    }
}
