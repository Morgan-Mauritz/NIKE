using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using NikeClientApp.ViewModels;

namespace NikeClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        MapPageViewModel ViewModel => BindingContext as MapPageViewModel;
        public MapPage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;

            BindingContext = new MapPageViewModel(DependencyService.Get<INaviService>());

            ResetStarColor();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize ViewModel
            ViewModel?.Init();
        }

        void ResetStarColor() // reset rating
        {
            ChangeStarColor(5, Color.Gray);
        }

        void ChangeStarColor(int starcount, Color color)
        {
            for (int i = 1; i <= starcount; i++)
            {
                (FindByName($"star{i}") as Label).TextColor = color;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e) //rating
        {
            ResetStarColor();
            Label clicked = sender as Label;
            ChangeStarColor(Convert.ToInt32(clicked.StyleId.Substring(4, 1)), Color.Yellow);
        }

        private void Searchbar_Focused(object sender, FocusEventArgs e)
        {
            Searchbar.Text = ""; 

        }

        //private async void SearchButt_Clicked(object sender, EventArgs e) //sök på plats
        //{
        //    Geocoder geoCoder = new Geocoder();

        //    IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(TBSearchbar.Text);

        //    Position position = approximateLocations.FirstOrDefault();
        //    string coordinates = $"{position.Latitude}, {position.Longitude}";
        //    CityName.Text = TBSearchbar.Text;

        //    MapSpan maps = new MapSpan(position, 1.10, 0.10);
        //    //Mapsample.MoveToRegion(maps);
        //}

        //private void PinButt_Clicked(object sender, EventArgs e)
        //{
        //    pinner = new Pin()
        //    {
        //        Label = "BlaBla",
        //        Address = "BlaStreet",
        //        Type = PinType.Place
        //    };

        //    //pinner.MarkerClicked += Pin_MarkerClicked;
        //}

        //private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e) //när man klickar på pinnen
        //{
        //    var ans = await DisplayAlert("Ta bort pin", "Vill du ta bort den valda pin?", "Ja", "Nej");
        //    if (ans == true)
        //    {
        //        var pin = sender as Pin;
        //        //Mapsample.Pins.Remove(ListOfPins.Where(x => x.Position == pin.Position).FirstOrDefault());
        //        ListOfPins.Remove(pin);
        //        AddPoiModal.IsVisible = false;
        //    }
        //}


        //    //if (star1.TextColor == Color.Gray)
        //    //{
        //    //    await DisplayAlert("Fel", "Du måste betygsätta.", "OK");
        //    //    return;
        //    //}

        //    Reset();

        //    AddPoiModal.IsVisible = false;
        //    await DisplayAlert("Grattis", "Du har nu lagt till en sevärdhet", "OK");
        //}

    }
}
