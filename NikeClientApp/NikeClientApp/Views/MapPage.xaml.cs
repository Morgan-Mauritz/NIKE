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
        public List<Pin> pinss = new List<Pin>();
        public Pin pinner { get; set; }
        //public event EventHandler<PinClickedEventArgs> changeee;
        private void PinButt_Clicked(object sender, EventArgs e)
        {

            pinner = new Pin()
            {
                Label = "BlaBla",
                Address = "BlaStreet",
                Type = PinType.Place
            };
            pinss.Add(pinner);
            //pinner.MarkerClicked += changeee;
            
        }

       
             
        
        private async void Mapsample_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (pinner != null)
            {
            pinner.Position = e.Position;
            Mapsample.Pins.Add(pinner);

                var ans = await DisplayAlert("Hej", "Hopp", "Ta bort Pin", "Lägg till"); //alternativ ta bort pin/ lägg till sevärdhet
                if (ans == true)
                {

                    Mapsample.Pins.Remove(pinner);
                    pinner = null;
                }
                else
                {
                    Mapsample.Pins.Add(pinner);
                    pinner = null;
                }

            }


        }
        private void Remove_Click(Pin pinn)
        {
            Mapsample.Pins.Remove(pinn);
        }

        private void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
           

            //DisplayAlert("heeeloo", "j", "h");
        }
        
    }
}