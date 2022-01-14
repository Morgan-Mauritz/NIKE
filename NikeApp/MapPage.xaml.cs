using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NikeApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Windows.UI.Xaml.Controls.Page
    {
        public MapPage()
        {
            this.InitializeComponent();
            Mapsample.Loaded += Mapsample_Loaded;
        }

        private async void Mapsample_Loaded(object sender, RoutedEventArgs e)
        {

            var center =
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = 11.66509,
                    Longitude = 78.154587

                });

            await Mapsample.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(center, 3000));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
