using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NikeClientApp.ViewModels
{
    internal class RegisterPageViewModel : BaseViewModel
    {
        //public Xamarin.Forms.Command NextPage => new Command(async () => await NavigationService.NavigateTo<MapViewModel>());
        public Command BackPage => new Command(async () => await NavigationService.NavigateTo<MainPageViewModel>());

        public RegisterPageViewModel(INaviService naviService) : base(naviService)
        {
        }
    }
}
