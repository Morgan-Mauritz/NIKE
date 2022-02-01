using NikeClientApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public Command NextPage => new Command(async () => await NavigationService.NavigateTo<MainPageViewModel>());
        public Command BackPage; // => new Command(async () => await NavigationService.GoBack());
        public ICommand AddPOI => new Command(async () => await AddPointOfInterest());

        public MapPageViewModel(INaviService naviService) : base(naviService)
        {
            
        }

        POI _poi = new POI() { Name = "hEJSAN" }; 

        public POI AddPoi { get => _poi; set { SetProperty(ref _poi, value);} }
        async Task AddPointOfInterest()
        {
           var poi1 = AddPoi; 
           
        }  


    }
}
