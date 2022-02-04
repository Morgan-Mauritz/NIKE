using NikeClientApp.Models;
using NikeClientApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICommand _RatingAmount => new Command((object sender) => RatingAmount(sender));
        public ICommand _SearchButton_Clicked => new Command(async () => await SearchButton_Clicked());

        public ICommand _StandardMapView => new Command(async () => await StandardMapView());
        public ICommand _SatelliteMapView => new Command(async () => await SatelliteMapView());
        public ICommand _HybridMapView => new Command(async () => await HybridMapView());

        HttpService<Models.Entry> httpClient = new HttpService<Models.Entry>();
        HttpService<Forecast> weatherClient = new HttpService<Forecast>(); 

        //Constructor
        #region Constructor
        public MapPageViewModel(INaviService naviService) : base(naviService)
        {
            map.MapClicked += MapClicked;
        }
        #endregion; 

        public async Task StandardMapView()
        {
             map.MapType = MapType.Street; 
        }
        public async Task SatelliteMapView()
        {
            map.MapType = MapType.Satellite;
        }
        public async Task HybridMapView()
        {
            map.MapType = MapType.Hybrid;
        }

        //Properties 
        #region Properties

        List<Pin> ListOfPins = new List<Pin>();
        public Pin pinner { get; set; }

        int _entryRating = 0;  
        public int EntryRating { get => _entryRating; set { SetProperty(ref _entryRating, value);} }

        Map _map = new Map();
        public Map map { get => _map; set { SetProperty(ref _map, value);} }

        POI _poi = new POI();
        public POI poiToAdd { get => _poi; set { SetProperty(ref _poi, value);} }

        Models.Entry _entry = new Models.Entry();
        public Models.Entry entryToAdd { get => _entry; set { SetProperty(ref _entry, value);} }

        private bool _addPoiModalIsVisible;
        public bool addPoiModalIsVisible { get { return _addPoiModalIsVisible; } set { SetProperty(ref _addPoiModalIsVisible, value); } }
        #endregion;

        string _searchBarText; 
        public string SearchBarText { get => _searchBarText; set { SetProperty(ref _searchBarText, value); } }

        string _cityResult = "Location"; 
        public string CityResult { get => _cityResult; set { SetProperty(ref _cityResult, value); } }

        //Methods
        #region Methods
        async Task<bool> AddPOI_Clicked()
        {
            if (!string.IsNullOrEmpty(poiToAdd.Name) && _entryRating > 0 && !string.IsNullOrEmpty(entryToAdd.Description)) 
            { 
                await PopulateEntry();
                poiToAdd.Category = "";

                try
                {
                    await httpClient.Post("entry", entryToAdd);
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
            {
                await App.Current.MainPage.DisplayAlert("Fel", "Du måste fylla i samtliga fält för att kunna lägga till en sevärdhet!", "OK"); 
            }
                return false;
        }

        private async Task SearchButton_Clicked()
        { 
            CityResult = SearchBarText;
            
            if(_cityResult == "Location")
            {
                return; 
            }

            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(SearchBarText);

            Position position = approximateLocations.FirstOrDefault();
            string coordinates = $"{position.Latitude}, {position.Longitude}";
           

            MapSpan maps = new MapSpan(position, 1.10, 0.10);
            map.MoveToRegion(maps);
        }

        private async Task PinIcon_Clicked()
        {
            pinner = new Pin()
            {
                Label = "",
                Address = "",
                Type = PinType.Place
            };
        }

        public async void MapClicked(object sender, MapClickedEventArgs e)
        {
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
                    await PopulatePOI(e.Position);
                    ListOfPins.Add(pinner);
                    pinner = null;
                }
            }
        }

        public void RatingAmount(object sender)
        {
            EntryRating = int.Parse(sender.ToString());
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
            string[] separator = {"\r\n"};
            string[] countryFromDataString = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return countryFromDataString[2];
        }

        public async Task PopulatePOI(Position position)
        {
            var geoCoder = new Geocoder();
            var Address = await geoCoder.GetAddressesForPositionAsync(position); 

            poiToAdd.Country = GetCountryFromDataString(Address.First());
            poiToAdd.Longitude = position.Longitude;
            poiToAdd.Latitude = position.Latitude;

            //Fetch city from weatherApi ((hack!)the geocoder doesn't provide a city properly)
            var response = await weatherClient.Get("forecast", $"?longitude={poiToAdd.Longitude}&latitude={poiToAdd.Latitude}");
            poiToAdd.City = response.Data.City;
        }

        public async Task PopulateEntry()
        {
            entryToAdd.POI = poiToAdd;
            entryToAdd.Rating = EntryRating;
            entryToAdd.UserName = "admin";
        }
        #endregion;
    }
}
