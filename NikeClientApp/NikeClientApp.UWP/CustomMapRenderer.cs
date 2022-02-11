using NikeClientApp.Models;
using NikeClientApp.UWP;
using NikeClientApp.ViewModels;
using NikeClientApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(BaseViewModel), typeof(CustomMapRenderer))]
namespace NikeClientApp.UWP
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        List<Pin> ListOfPins_From_MapPageViewModel;
        XamarinMapOverlay mapOverlay;
        bool xamarinOverlayShown = false;
        BaseViewModel formsMap { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e) //Skapas automatiskt när kartan skapas
        {
            base.OnElementChanged(e);

            formsMap = (BaseViewModel)e.NewElement;
            nativeMap = Control as MapControl;

            ListOfPins_From_MapPageViewModel = MapPageViewModel.ListOfPins;

            nativeMap.Children.Clear(); //Tar bort UWP svarta vanliga PIN
            nativeMap.MapElementClick += OnMapElementClick;

            formsMap.MapClicked += OnMapClicked;

            MapPageViewModel.ShowPinsEventHandler += MapPageViewModel_ShowPins;

            formsMap = MapPageViewModel.MPVM;
        }

        private void MapPageViewModel_ShowPins(object sender, PaginationResponse<ObservableCollection<POI>> e)
        {
            foreach (var item in e.Data)
            {
                nativeMap.Children.Clear();
                var snPosition = new BasicGeoposition { Latitude = item.Latitude, Longitude = item.Longitude };
                var snPoint = new Geopoint(snPosition);

                var mapIcon = new MapIcon();
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin.png"));
                mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.Location = snPoint;
                mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                nativeMap.MapElements.Add(mapIcon);
            }
        }

        //Denna triggas när man klickar någonstans på kartan.
        private async void OnMapClicked(object sender, MapClickedEventArgs args)
        {
            if (ListOfPins_From_MapPageViewModel.Count != 0 || MapPageViewModel.pinner != null)
            {
                var MapPageVM = formsMap as MapPageViewModel;

                //Andra kalla om MapClicked i MapPageViewModel
                if (await MapPageVM.MapClicked(sender, args))
                {
                    nativeMap.Children.Clear();
                    var snPosition = new BasicGeoposition { Latitude = args.Position.Latitude, Longitude = args.Position.Longitude };
                    var snPoint = new Geopoint(snPosition);

                    var mapIcon = new MapIcon();
                    mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin.png"));
                    mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    mapIcon.Location = snPoint;
                    mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                    nativeMap.MapElements.Add(mapIcon);

                    //Här sätts lon och lat på senaste Pin skapad.
                    var lastPin = ListOfPins_From_MapPageViewModel.Last();
                    lastPin.Position = args.Position;
                }
            }
        }

        //När man klickar på ett element/Pin så triggas denna metod
        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapIcon != null)
            {
                if (!xamarinOverlayShown)
                {
                    //Kollar om custompinnen finns.
                    var customPin = GetCustomPin(mapIcon.Location.Position);
                    if (customPin == null)
                    {
                        throw new Exception("Custom pin not found");
                    }

                    if (customPin.Position.Longitude == mapIcon.Location.Position.Longitude &
                        customPin.Position.Latitude == mapIcon.Location.Position.Latitude)
                    {
                        if (mapOverlay == null)
                        {
                            //Här inne sätts Label och Adress på info rutan
                            mapOverlay = new XamarinMapOverlay(customPin);

                        }

                        //Denna del sätter en info ruta under pinnen.
                        var snPosition = new BasicGeoposition { Latitude = customPin.Position.Latitude, Longitude = customPin.Position.Longitude };
                        var snPoint = new Geopoint(snPosition);

                        nativeMap.Children.Add(mapOverlay);
                        MapControl.SetLocation(mapOverlay, snPoint);
                        MapControl.SetNormalizedAnchorPoint(mapOverlay, new Windows.Foundation.Point(0.5, 1.0));
                        xamarinOverlayShown = true;
                    }
                }
                else
                {
                    nativeMap.Children.Remove(mapOverlay);
                    mapOverlay = null;
                    xamarinOverlayShown = false;
                }
            }
        }

        Pin GetCustomPin(BasicGeoposition position)
        {
            var pos = new Position(position.Latitude, position.Longitude);
            foreach (var pin in ListOfPins_From_MapPageViewModel)
            {
                if (pin.Position == pos)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}
