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
        public static MapPageViewModel CustomMap { get; private set; }
        public MapPage()
        {
            InitializeComponent();
            BackgroundColor = Color.Black;

            BindingContext = new MapPageViewModel(DependencyService.Get<INaviService>());
            CustomMap = customMap;
            ResetStarColor();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize ViewModel
            ViewModel?.Init();
            await ViewModel.InitAsync();
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            EntryPoi.Text = "";
            EntryCommentPoi.Text = "";
            ResetStarColor();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CommentOnEntry.Text = "";
        }

        private void AddEntry_Clicked(object sender, EventArgs e)
        {
            EntryCommentExistingPoi.Text = "";
        }
    }
}
