using NikeClientApp.Models;
using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
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


        public Command NextPage => new Command(async () => await NavigationService.NavigateTo<UserPageViewModel>());
        public Command RegisterPage => new Command(async () => await NavigationService.NavigateTo<RegisterPageViewModel>());

        public async Task OnLogIn()
        {

            var responseData = await userClient.Post("Authorization/login", User);

            UserApi.ApiKey = responseData.Data.ApiKey;

            await NavigationService.NavigateTo<UserPageViewModel>();

        }

        public MainPageViewModel(INaviService naviService) : base(naviService)
        {
            userClient = new HttpService<User>();
            User = new User();
        }
    
    }
}
