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
        public Pin pinner { get; set; }
        public event EventHandler<PinClickedEventArgs> changeee;
        private void PinButt_Clicked(object sender, EventArgs e)
        {

            pinner = new Pin()
            {
                Label = "BlaBla",
                Address = "BlaStreet",
                Type = PinType.Place
            };
           
            pinner.MarkerClicked += changeee;
        }

        protected virtual void onp()
        {
            
            DisplayAlert("heeeloo", "j", "h");
        }
             
        
        private void Mapsample_MapClicked(object sender, MapClickedEventArgs e)
        {
            if (pinner != null)
            {
            pinner.Position = e.Position;
            Mapsample.Pins.Add(pinner);

            
            }
            
        }
        private void Remove_Click(Pin pinn)
        {
            Mapsample.Pins.Remove(pinn);
        }

        private void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            DisplayAlert("heeeloo", "j", "h");
        }
        //private async void ClickedPin()
        //{
        //    //if (pinner.MarkerClicked += )
        //    //{

        //    //}
        //    var ans = await DisplayAlert("Varning", "För att kunna lägga till sevärdheter, kommentera och betygsätta, \nmåste du först registrera dig.", "OK", "Avbryt");
        //    if (ans == true)
        //    {
        //            Remove_Click(pinner);
        //    }
        //    else
        //    {

        //    }


        //}
    }
}