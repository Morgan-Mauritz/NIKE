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

        public async Task OnLogIn()
        {
            try
            {
            if (string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Du måste fylla i alla fält", "OK");
                return;
            }
            if (!Regex.IsMatch(User.Email, @"^[a-zåäöA-ZÅÄÖ][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]@[a-zåäöA-ZÅÄÖ0-9][\w\.-]*[a-zåäöA-ZÅÄÖ0-9]\.[a-zåäöA-ZÅÄÖ][a-zåäöA-ZÅÄÖ\.]*[a-zåäöA-ZÅÄÖ]$"))
            {
                await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Ej korrekt email", "OK");
                return;
            }
            var responseData = await userClient.Post("authorization/login", User, false);
            UserApi.ApiKey = responseData.Data.ApiKey;
            }
            catch (HttpRequestException)
            {
                await Application.Current.MainPage.DisplayAlert("Felmeddelande", "Fel inloggningsuppgifter", "OK");
                return;
            }


            //TODO: Change to mappage
            await NavigationService.NavigateTo<UserPageViewModel>();

        }

        public MainPageViewModel(INaviService naviService) : base(naviService)
        {
            userClient = new HttpService<User>();
            User = new User();
        }

    }
}
