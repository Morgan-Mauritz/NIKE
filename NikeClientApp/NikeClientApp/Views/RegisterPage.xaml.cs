using System;
using NikeClientApp.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using NikeClientApp.Services;
using NikeClientApp.Models;
using NikeClientApp.Encryption;

namespace NikeClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        HttpService<User> httpClient = new HttpService<User>(); 
        public RegisterPage()
        {
            InitializeComponent();
            
        }

        private async void BTRegister_Clicked(object sender, EventArgs e)
        {
            if (TBFirstname.Text == null || TBLastname.Text == null || TBEmail.Text == null || TBUsername.Text == null || TBPassword.Text == null || TBRepatPassword.Text == null)
            {
                await DisplayAlert("Fel", "Du måste fylla alla fält ", "OK");
                return;
            }
            if (!Regex.IsMatch(TBFirstname.Text, @"^[a-zåäöA-ZÅÄÖ]+$"))
            {
              await DisplayAlert ("Förnamn", "Ange bara bokstäver", "OK");
                return;
            }
            if (!Regex.IsMatch(TBLastname.Text, @"^[a-zåäöA-ZÅÄÖ]+$"))
            {
                await DisplayAlert ("Efternamn", "Ange bara bokstäver", "OK");
                return;
            }
            if (!Regex.IsMatch(TBEmail.Text, @"^[a-zåäöA-ZÅÄÖ][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]@[a-zåäöA-ZÅÄÖ0-9][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]\.[a-zåäöA-ZÅÄÖ][a-zåäöA-ZÅÄÖ\.]*[a-zåäöA-ZÅÄÖ]$"))
            {
                await DisplayAlert ("Email", "Ange korrekt e-mail", "OK");
                return;
               
            }
            if (TBUsername.Text.Length == 0)
            {
               await DisplayAlert("Användarnamn", "Skriv in användarnamn", "OK");
                return;

            }
            if (!Regex.IsMatch(TBPassword.Text, @"^(?=.*?[A-ZÅÄÖ])(?=.*?[a-zåäö])(?=.*?[0-9]).{8,}$"))
            {
               await DisplayAlert("Lösenord", "Lösenordet ska vara minst 8 tecken.\nAnge minst en stor och liten bokstav samt en siffra.", "OK");
                return;
              
            }
            if (TBRepatPassword.Text != TBPassword.Text)
            {
                await DisplayAlert("Lösenord", "Lösenorden matchar inte.", "OK");
                return;

            }

            await httpClient.Post("user", new User
            {
                Firstname = TBFirstname.Text,
                Lastname = TBLastname.Text,
                Email = TBEmail.Text,
                Username = TBUsername.Text,
                Password = Encrypt.EncryptMessage(TBPassword.Text)
            }, false);

            await DisplayAlert("Grattis", "Du har nu registrerat dig hos NikeApp.\nLogga in för att fortsätta", "OK"  );
            await Navigation.PushAsync(new MainPage());
        }
    }
}