using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NikeClientApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Xamarin.Forms.Command NextPage => new Command(async () => await NavigationService.NavigateTo<MapPageViewModel>());
        public Command RegisterPage => new Command(async () => await NavigationService.NavigateTo<RegisterPageViewModel>());

        public MainPageViewModel(INaviService naviService) : base(naviService)
        {
        }
    
    }
}
