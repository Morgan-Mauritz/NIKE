using NikeClientApp.Models;
using NikeClientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NikeClientApp.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public Command NextPage => new Command(async () => await NavigationService.NavigateTo<MainPageViewModel>());
        public Command BackPage; // => new Command(async () => await NavigationService.GoBack());
        public ICommand AddPOI => new Command(async () => await AddPointOfInterest());
        //public ICommand ShowAddPoiModal => new Command(async () => await ShowModalWhenClicked());

        List<Pin> ListOfPins = new List<Pin>();
        public Pin pinner { get; set; }


        public MapPageViewModel(INaviService naviService) : base(naviService)
        {
            map.MapClicked += MapClicked;
        }

        Map _map = new Map();
        public Map map
        {
            get => _map;

            set
            {
                SetProperty(ref _map, value);
            }
        }


        POI _poi = new POI();
        public POI poiToAdd
        {
            get => _poi;

            set
            {
                SetProperty(ref _poi, value);
            }
        }

        async Task AddPointOfInterest()
        {
            var poi1 = poiToAdd;
        }

        private bool _addPoiModalIsVisible;
        public bool addPoiModalIsVisible
        {
            get { return _addPoiModalIsVisible; }

            set 
            {
                SetProperty(ref _addPoiModalIsVisible, value);
            }
        }
        //public async Task ShowModalWhenClicked()
        //{
        //    if (addPoiModalIsVisible == true) addPoiModalIsVisible = false;
        //    addPoiModalIsVisible = true;
        //}
        public async void MapClicked(object sender, MapClickedEventArgs e)
        {
            var geoCoder = new Geocoder();
            var Address = await geoCoder.GetAddressesForPositionAsync(e.Position);

            if (pinner != null)
            {
                pinner.Position = e.Position;
                //Mapsample.Pins.Add(pinner);

                var ans = await App.Current.MainPage.DisplayAlert("Hej", "Vill du lägga till en pin?", "Ja", "Nej");
                if (ans != true)
                {
                    //Mapsample.Pins.Remove(pinner);
                    pinner = null;
                }
                else
                {
                    addPoiModalIsVisible = true;
                    //var Address = await geoCoder.GetAddressesForPositionAsync(e.Position); // TODO: Separate adress/City/Country in method, post to db
                    ListOfPins.Add(pinner);
                    pinner = null;
                }
            }
        }
        private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e) //när man klickar på pinnen
        {
            var ans = await App.Current.MainPage.DisplayAlert("Ta bort pin", "Vill du ta bort den valda pin?", "Ja", "Nej");
            if (ans == true)
            {
                var pin = sender as Pin;
                //Mapsample.Pins.Remove(ListOfPins.Where(x => x.Position == pin.Position).FirstOrDefault());
                ListOfPins.Remove(pin);
                addPoiModalIsVisible = false;
            }
        }
    }
}
