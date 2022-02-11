using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms.Maps;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NikeClientApp.UWP
{
    public sealed partial class XamarinMapOverlay : UserControl
    {
        Pin customPin;

        public XamarinMapOverlay(Pin pin)
        {
            this.InitializeComponent();
            customPin = pin;
            SetupData();
        }

        void SetupData()
        {
            Label.Text = customPin.Label;
            Address.Text = customPin.Address;
        }

        private async void OnInfoButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            //await Launcher.LaunchUriAsync(new Uri(customPin.Url));
        }

        public async void BtnDeletePin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //MainPage.DeleteAnswer(sender, List);
        }
    }
}
