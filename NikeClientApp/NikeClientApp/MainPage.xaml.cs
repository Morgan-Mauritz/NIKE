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

namespace NikeClientApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
      

        private async void BtnLogIn_Clicked(object sender, EventArgs e)
        {
            //Vertification that the user exist, if true than send to next page, if no write error message.
            if (!Regex.IsMatch(entryEmail.Text, @"^[a-zåäöA-ZÅÄÖ][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]@[a-zåäöA-ZÅÄÖ0-9][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]\.[a-zåäöA-ZÅÄÖ][a-zåäöA-ZÅÄÖ\.]*[a-zåäöA-ZÅÄÖ]$"))
            {
                await DisplayAlert("Email", "Ange korrekt e-mail", "OK");
                return;

            }



            await Navigation.PushAsync(new MapPage());
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
