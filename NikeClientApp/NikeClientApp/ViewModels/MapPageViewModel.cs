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
        public Xamarin.Forms.Command NextPage; // => new Command(async () => await NavigationService.NavigateTo<MapViewModel>());
        public Command BackPage; // => new Command(async () => await NavigationService.GoBack());
        public ICommand AddPOI => new Command(async () => await AddPointOfInterest());

        public MapPageViewModel(INaviService naviService) : base(naviService)
        {

        }

        public POI AddPoi { get; set; }
        async Task AddPointOfInterest()
        {
           var poi1 = AddPoi; 
           
        }  


    }
}
