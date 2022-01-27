using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
namespace NikeClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;

            Reset();

        }

        List<Pin> ListOfPins = new List<Pin>();
        public Pin pinner { get; set; }

        void Reset()
        {
            ChangeTextColor(5, Color.Gray);
        }

        void ChangeTextColor(int starcount, Color color)
        {
            for (int i = 1; i <= starcount; i++)
            {
                (FindByName($"star{i}") as Label).TextColor = color;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Reset();
            Label clicked = sender as Label;
            ChangeTextColor(Convert.ToInt32(clicked.StyleId.Substring(4, 1)), Color.Yellow);
        }

        private async void SearchButt_Clicked(object sender, EventArgs e)
        {
            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(TBSearchbar.Text);
            Position position = approximateLocations.FirstOrDefault();
            string coordinates = $"{position.Latitude}, {position.Longitude}";
            CityName.Text = TBSearchbar.Text;

            MapSpan maps = new MapSpan(position, 1.10, 0.10);
            Mapsample.MoveToRegion(maps);


        }

        private void PinButt_Clicked(object sender, EventArgs e)
        {

            pinner = new Pin()
            {
                Label = "BlaBla",
                Address = "BlaStreet",
                Type = PinType.Place
            };

            pinner.MarkerClicked += Pin_MarkerClicked;

        }




        private async void Mapsample_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (pinner != null)
            {
                pinner.Position = e.Position;
                Mapsample.Pins.Add(pinner);

                var ans = await DisplayAlert("Hej", "Vill du lägga till en pin?", "Ja", "Nej"); //alternativ ta bort pin/ lägg till sevärdhet
                if (ans != true)
                {

                    Mapsample.Pins.Remove(pinner);
                    pinner = null;
                }
                else
                {
                    Jonsson.IsVisible = true;

                    ListOfPins.Add(pinner);
                    pinner = null;
                }
            }
        }
        private void Remove_Click(Pin pinn)
        {
            Mapsample.Pins.Remove(pinn);
        }



        private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            var ans = await DisplayAlert("Ta bort pin", "Vill du ta bort den valda pin?", "Ja", "Nej"); //alternativ ta bort pin/ lägg till sevärdhet
            if (ans == true)
            {
                var pin = sender as Pin;
                Mapsample.Pins.Remove(ListOfPins.Where(x => x.Position == pin.Position).FirstOrDefault());
                ListOfPins.Remove(pin);
                Jonsson.IsVisible = false;
            }
        }

        private async void AddLoc_Clicked(object sender, EventArgs e)
        {
            if (EntryPoi.Text == null || EntryCommentPoi.Text == null)
            {
                await DisplayAlert("Fel", "Du måste fylla alla fält ", "OK");
                return;
            }
            Jonsson.IsVisible = false;
        }



        private void streetcommand()
        {
            Mapsample.MapType = MapType.Street;
        }
        private void Satellitecommand()
        {
            Mapsample.MapType = MapType.Satellite;
        }
        private void Hybridcommand()
        {
            Mapsample.MapType = MapType.Hybrid;
        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            streetcommand();
        }

        private void btn2_Clicked(object sender, EventArgs e)
        {
            Satellitecommand();
        }

        private void btn3_Clicked(object sender, EventArgs e)
        {
            Hybridcommand();
        }
    }
}
