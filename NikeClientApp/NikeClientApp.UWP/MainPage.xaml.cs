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

namespace NikeClientApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("mp9cLB5Z0jbyVLeJBLeQ~SMN9mdi8V7uaRDipVAXKqQ~Arj7gT48yZRpTGyMrrW2ZmJpdBnc6BseHRr-Rwy-9aB4s9pnO0XeglEdNErPe4h5");
            Windows.Services.Maps.MapService.ServiceToken = "mp9cLB5Z0jbyVLeJBLeQ~SMN9mdi8V7uaRDipVAXKqQ~Arj7gT48yZRpTGyMrrW2ZmJpdBnc6BseHRr-Rwy-9aB4s9pnO0XeglEdNErPe4h5";
            LoadApplication(new NikeClientApp.App());
        }
    }
}
