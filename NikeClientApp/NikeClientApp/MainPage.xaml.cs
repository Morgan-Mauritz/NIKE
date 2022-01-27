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

namespace NikeClientApp
{
    public partial class MainPage : ContentPage
    {
        HttpService<User, Response<User>> httpClient = new HttpService<User, Response<User>>();

        public MainPage()
        {
            InitializeComponent();
            
        }
        private async void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            //Vertification that the user exist, if true than send to next page, if no write error message.

            if (entryEmail.Text == null)
            {
                await DisplayAlert("Fel", "Du måste fylla alla fält ", "OK");
                return;
            }
            if (!Regex.IsMatch(entryEmail.Text, @"^[a-zåäöA-ZÅÄÖ][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]@[a-zåäöA-ZÅÄÖ0-9][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]\.[a-zåäöA-ZÅÄÖ][a-zåäöA-ZÅÄÖ\.]*[a-zåäöA-ZÅÄÖ]$"))
            {
                await DisplayAlert("Email", "Ange korrekt e-mail", "OK");
                return;

            }

            var responseData = await httpClient.Post( new User { Email = entryEmail.Text, Password = entryPassword.Text }, "Authorization/login" );

            UserApi.ApiKey = responseData.Data.ApiKey;

            await Navigation.PushAsync(new MapPage());
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
            else
            {
                await Navigation.PushAsync(new MainPage());
            }

            
        }
    }
}
