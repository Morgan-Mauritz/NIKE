using NikeClientApp.Encryption;
using NikeClientApp.Models;
using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NikeClientApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        HttpService<User> userClient;

        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        public Command LogIn => new Command(async () => await OnLogIn());
        public Command RegisterPage => new Command(async () => await NavigationService.NavigateTo<RegisterPageViewModel>());
        public Command MapPage => new Command(async () => await OffLineLogIn());
        public async Task OnLogIn()
        {
            try
            {
                if (string.IsNullOrEmpty(User.Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Du måste fylla i alla fält", "OK");
                    return;
                }
                if (!Regex.IsMatch(User.Email, 
                    @"^[a-zåäöA-ZÅÄÖ][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]@[a-zåäöA-ZÅÄÖ0-9][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]\.[a-zåäöA-ZÅÄÖ][a-zåäöA-ZÅÄÖ\.]*[a-zåäöA-ZÅÄÖ]$"))
                {
                    await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Ej korrekt email", "OK");
                    return;
                }
                    UserOffLine.LoggedIn = true;
                    User.Password = Encrypt.EncryptMessage(User.PasswordText);
                    var responseData = await userClient.Post("authorization/login", User, false);
                    UserApi.ApiKey = responseData.Data.ApiKey;
                    await NavigationService.NavigateTo<MapPageViewModel>();
            }
            catch (HttpRequestException)
            {
                await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Fel inloggningsuppgifter", "OK");
                return;
            }
        }

        public async Task OffLineLogIn()
        {
            var ans = await App.Current.MainPage.DisplayAlert("Varning", "För att kunna lägga till sevärdheter, kommentera och betygsätta, \nmåste du först registrera dig.", "OK", "Avbryt");
            if (ans == true)
            {
                UserOffLine.LoggedIn = false;
                //TODO: Change to mappage
                await NavigationService.NavigateTo<MapPageViewModel>();
            }
        }

        public MainPageViewModel(INaviService naviService) : base(naviService)
        {
            userClient = new HttpService<User>();
            User = new User();
        }

    }
}
