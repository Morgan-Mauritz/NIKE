﻿using NikeClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NikeClientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel(DependencyService.Get<INaviService>());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = OSAppTheme.Dark;
        }
    }
}