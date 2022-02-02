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
                poiToAdd.Longitude = pinner.Position.Longitude;
                poiToAdd.Latitude = pinner.Position.Latitude;
                poiToAdd.City = "";
                poiToAdd.Country = "";
                poiToAdd.Category = "";
                await httpClient.Post("poi", poiToAdd);
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
        //public async Task ShowModalWhenClicked()
        //{
        //    if (addPoiModalIsVisible == true) addPoiModalIsVisible = false;
        //    addPoiModalIsVisible = true;
        //}

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
                    FormatAddressString(Address.First());
                    ListOfPins.Add(pinner);
                    //pinner = null;

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

        //private async Task AddPOI_Clicked() //lägg till sevärdhet
        //{
        //    if (EntryPoi.Text == null || EntryCommentPoi.Text == null || star1.TextColor == Color.Gray)
        //    {
        //        await DisplayAlert("Fel", "Du måste fylla alla fält och betygsätta. ", "OK");
        //        return;
        //    }


        //    //if (star1.TextColor == Color.Gray)
        //    //{
        //    //    await DisplayAlert("Fel", "Du måste betygsätta.", "OK");
        //    //    return;
        //    //}

        //    Reset();

        //    AddPoiModal.IsVisible = false;
        //    await App.Current.MainPage.DisplayAlert("Grattis", "Du har nu lagt till en sevärdhet", "OK");
        //}

        public string FormatAddressString(string inputString)
        {
            string city;
            string country;
            string streetAddress;
            string[] separators = {"\r\n"};
            string[] splitStrings = inputString.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            streetAddress = splitStrings[0];
            city = splitStrings[1];  //TODO: Här kommer även postnr och annat orelevant info in.
            country = splitStrings[2];

            return "";
        }
    }
}
