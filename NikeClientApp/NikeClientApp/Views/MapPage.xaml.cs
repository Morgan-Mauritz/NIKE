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
        List<Pin> Location = new List<Pin>();
        public MapPage()
        {
            InitializeComponent();

            
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
            Position pos = new Position();
            MapClickedEventArgs clicli = new MapClickedEventArgs(pos);
            Mapsample_MapClicked(sender, clicli);
        }

        

        private void Mapsample_MapClicked(object sender, MapClickedEventArgs e)
        {
            Pin pinner = new Pin()
            {
                Position = e.Position,
                Label = "Boardwalk",
                Address = "Santa Cruz",
                Type = PinType.Place
            };
           
            Mapsample.Pins.Add(pinner);

            //Remove_Click(pinner);
        }
        private void Remove_Click(Pin pinn)
        {
            Mapsample.Pins.Remove(pinn);
        }
    }
}