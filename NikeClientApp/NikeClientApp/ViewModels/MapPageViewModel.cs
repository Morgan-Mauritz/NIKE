using NikeClientApp.Models;
using NikeClientApp.Services;
using NikeClientApp.Views;
using System;
using System.Collections.Generic;

using System.Linq;
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
        public ICommand _AddPOI_Clicked => new Command(async () => await AddPOI_Clicked());
        public ICommand _PinIcon_Clicked => new Command(async () => await PinIcon_Clicked());
        //public ICommand _ratingAmount => new Command(async () => await );

        //public ICommand ShowAddPoiModal => new Command(async () => await ShowModalWhenClicked());

        HttpService<POI> httpClient = new HttpService<POI>();
        HttpService<Forecast> weatherClient = new HttpService<Forecast>(); 

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

        async Task<bool> AddPOI_Clicked()
        {
            if (poiToAdd.Name != null || poiToAdd.Comment != null )
            {
               
                poiToAdd.Category = "";

                try
                {
                    await httpClient.Post("poi", poiToAdd);
                }
                catch(Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                    map.Pins.Remove(map.Pins.Last()); 
                }
                addPoiModalIsVisible = false; 
                return true;
            }
            else
                return false;
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
        
        private async Task PinIcon_Clicked()
        {
            pinner = new Pin()
            {
                Label = "",
                Address = "",
                Type = PinType.Place
            };

            //pinner.MarkerClicked += Pin_MarkerClicked;
        }

        public async void MapClicked(object sender, MapClickedEventArgs e)
        {
            var geoCoder = new Geocoder();

            if (pinner != null)
            {
                pinner.Position = e.Position;
                map.Pins.Add(pinner);

                var ans = await App.Current.MainPage.DisplayAlert("Hej", "Vill du lägga till en pin?", "Ja", "Nej");
                if (ans != true)
                {
                    map.Pins.Remove(pinner);
                    pinner = null;
                }
                else
                {
                    addPoiModalIsVisible = true;
                    var Address = await geoCoder.GetAddressesForPositionAsync(e.Position); // TODO: Separate adress/City/Country in method, post to db
                    //setting the country prop for the PointOfInterest
                    poiToAdd.Country = GetCountryFromDataString(Address.First());
                    poiToAdd.Longitude = e.Position.Longitude;
                    poiToAdd.Latitude = e.Position.Latitude;
                    //Fetch city from weatherApi
                    var response = await weatherClient.Get("forecast", $"?longitude={poiToAdd.Longitude}&latitude={poiToAdd.Latitude}");
                    poiToAdd.City = response.Data.City;  
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

        public string GetCountryFromDataString(string dataString)
        {
            string country;
            string streetAddress;
            string[] separator = {"\r\n"};
            string[] splitStrings = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            country = splitStrings[2];
            streetAddress = splitStrings[0]; 

            return country;
        }
    }
}
