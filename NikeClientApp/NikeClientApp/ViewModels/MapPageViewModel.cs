using NikeClientApp.Models;
using NikeClientApp.Services;
using NikeClientApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Entry = NikeClientApp.Models.Entry;

namespace NikeClientApp.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        //Commands
        public Command NextPage => new Command(async () => await NavigationService.NavigateTo<MainPageViewModel>());
        public Command NavToUserPage => new Command(async () => await NavigationService.NavigateTo<UserPageViewModel>());
        public ICommand _AddPOI_Clicked => new Command(async () => await AddPOI_Clicked());
        public ICommand _PinIcon_Clicked => new Command(async () => await PinIcon_Clicked());
        public ICommand _RatingAmount => new Command((object sender) => RatingAmount(sender));
        public ICommand _SearchButton_Clicked => new Command(async () => await SearchButton_Clicked());
        public ICommand _StandardMapView => new Command(async () => await StandardMapView());
        public ICommand _SatelliteMapView => new Command(async () => await SatelliteMapView());
        public ICommand _HybridMapView => new Command(async () => await HybridMapView());
        public ICommand POISelected => new Command(async (param) => await OnPOISelected(param));
        public ICommand _BackArrowClicked => new Command(async () => await BackArrowClicked());
        public ICommand _FoldFrameClicked => new Command(async (object sender) => await FoldFrameClicked((Frame)sender));
        public ICommand _EntryButton_Clicked => new Command(async () => await EntryButton_Clicked());
        public ICommand _CenterOnUser => new Command(async () => await CenterOnUser());
        public ICommand _SwitchWeatherEntry => new Command(() => SwitchWeatherEntry());
        public ICommand LikeButtonClicked => new Command(async (object sender) => await LikeButton_Clicked(sender));
        public ICommand GetNextEntries => new Command(async () => await OnGetNextEntries());
        public ICommand GetPreviousEntries => new Command(async () => await OnGetPreviousEntries());
        public ICommand CommentButtonClicked => new Command(async (object sender) => await OnCommentButtonClicked(sender));
        public ICommand _OpenEntryComment => new Command(async () => await OpenEntryComment());
        public ICommand _PostComment => new Command(async () => await PostComment());

        //Connection to the httpservice
        #region HttpService
        HttpService<Models.Entry> _entryClient = new HttpService<Models.Entry>();
        HttpService<Forecast> weatherClient = new HttpService<Forecast>();
        HttpService<POI> poiListClient = new HttpService<POI>();
        HttpService<LikeDislikeEntry> httpClientLike = new HttpService<LikeDislikeEntry>();
        HttpService<User> userClient = new HttpService<User>();
        HttpService<Comment> commentClient = new HttpService<Comment>();
        #endregion

        public static MapPageViewModel MPVM { get; set; }

        public static event EventHandler<PaginationResponse<ObservableCollection<POI>>> ShowPinsEventHandler;

        protected virtual void OnShowPinsEventHandler(PaginationResponse<ObservableCollection<POI>> listOfPOI)
        {
            ShowPinsEventHandler?.Invoke(this, listOfPOI);
        }

        //Constructor
        #region Constructor
        public MapPageViewModel(INaviService naviService) : base(naviService)
        {
            MPVM = this;
            Categories = new List<Category> {
                new Category("Hotell"),
                new Category("Strand"),
                new Category("Restaurang"),
                new Category("Landmärken"),
                new Category("Park"),
                new Category("Övrigt") };
        }
        public MapPageViewModel()
        {

        }
        #endregion; 

        //Properties 
        #region Properties

        private List<Category> _categories;

        public List<Category> Categories { get { return _categories; } set { SetProperty(ref _categories, value); } }

        public static List<Pin> ListOfPins = new List<Pin>();
        public static Pin pinner { get; set; }

        int _entryRating = 0;
        public int EntryRating { get => _entryRating; set { SetProperty(ref _entryRating, value); } }

        POI _poi = new POI();
        public POI poiToAdd { get => _poi; set { SetProperty(ref _poi, value); } }

        Models.Entry _entry = new Models.Entry();
        public Models.Entry entryToAdd { get => _entry; set { SetProperty(ref _entry, value); } }

        private bool _addPoiModalIsVisible;
        public bool addPoiModalIsVisible { get { return _addPoiModalIsVisible; } set { SetProperty(ref _addPoiModalIsVisible, value); } }

        private bool _addEntryModalIsVisible = false;
        public bool addEntryModalIsVisible { get => _addEntryModalIsVisible; set { SetProperty(ref _addEntryModalIsVisible, value); } }

        private bool _pOIListIsVisible;

        public bool POIListIsVisible { get => _pOIListIsVisible; set { SetProperty(ref _pOIListIsVisible, value); } }

        private bool _entryListIsVisible;
        public bool EntryListIsVisible { get => _entryListIsVisible; set { SetProperty(ref _entryListIsVisible, value); } }

        private bool _entryButtonIsVisible;
        public bool EntryButtonIsVisible { get => _entryButtonIsVisible; set { SetProperty(ref _entryButtonIsVisible, value); } }

        private bool _backArrowIsVisible;
        public bool BackArrowIsVisible { get => _backArrowIsVisible; set { SetProperty(ref _backArrowIsVisible, value); } }

        private bool _weatherListIsVisible;
        private bool _foldButtonIsVisible;
        public bool FoldButtonIsVisible { get => _foldButtonIsVisible; set { SetProperty(ref _foldButtonIsVisible, value); } }

        public bool WeatherListIsVisible { get => _weatherListIsVisible; set { SetProperty(ref _weatherListIsVisible, value); } }



        private bool _foldInFrameIsVisible = true;
        public bool FoldInFrameIsVisible { get => _foldInFrameIsVisible; set { SetProperty(ref _foldInFrameIsVisible, value); } }

        private bool _previousEntriesVisible = true;
        public bool PreviousEntriesVisible { get => _previousEntriesVisible; set { SetProperty(ref _previousEntriesVisible, value); } }

        private bool _nextEntriesVisible = true;
        public bool NextEntriesVisible { get => _nextEntriesVisible; set { SetProperty(ref _nextEntriesVisible, value); } }

        private bool _commentListIsVisible;

        public bool CommentListIsVisible { get => _commentListIsVisible; set { SetProperty(ref _commentListIsVisible, value); } }

        private string _commentOnEntry = null;
        public string CommentOnEntry { get => _commentOnEntry; set { SetProperty(ref _commentOnEntry, value); } }

        private bool _commentOnEntryModalIsVisible = false;
        public bool CommentOnEntryModalIsVisible { get => _commentOnEntryModalIsVisible; set { SetProperty(ref _commentOnEntryModalIsVisible, value); } }


        POI _selectedPOI;
        public POI SelectedPOI
        {
            get => _selectedPOI; set
            {
                SetProperty(ref _selectedPOI, value);
                ShowEntriesForPOI();
            }
        }


        Entry _selectedEntry;

        public Entry SelectedEntry { get => _selectedEntry; set { SetProperty(ref _selectedEntry, value); } }

        int _currentWeather;

        public int CurrentWeather { get => _currentWeather; set { SetProperty(ref _currentWeather, value); } }


        string _searchBarText;
        public string SearchBarText { get => _searchBarText; set { SetProperty(ref _searchBarText, value); } }

        string _titleResult = "Location";
        public string TitleResult { get => _titleResult; set { SetProperty(ref _titleResult, value); } }

        string _avgRating;

        public string AvgRating { get => _avgRating; set { SetProperty(ref _avgRating, value); } }

        private string _SwitchWeatherTxt;
        public string LikeButtonNotFilled = @".\Assets\LikeButtonNotFilled.png";

        public string SwitchWeatherTxt { get => _SwitchWeatherTxt; set { SetProperty(ref _SwitchWeatherTxt, value); } }
        public string LikeButtonFilled = @".\Assets\LikeButtonFilled.png";
        public PaginationResponse<ObservableCollection<Models.Entry>> EntryPagination { get; set; }

        //Backing fields keeping track of the current selected 
        string _cityName;
        public string CityName { get => _cityName; set { SetProperty(ref _cityName, value); } }
        string _countryName;
        public string CountryName { get => _countryName; set { SetProperty(ref _countryName, value); } }
        string _poiName;
        public string POIName { get => _poiName; set { SetProperty(ref _poiName, value); } }
        double _longitude;
        public double Longitude { get => _longitude; set { SetProperty(ref _longitude, value); } }
        double _latitude;
        public double Latitude { get => _latitude; set { SetProperty(ref _latitude, value); } }
        ObservableCollection<Entry> _entries;
        public ObservableCollection<Entry> Entries { get => _entries; set { SetProperty(ref _entries, value); } }


        private PaginationResponse<ObservableCollection<POI>> _listOfPOI;
        public PaginationResponse<ObservableCollection<POI>> ListOfPOI { get => _listOfPOI; set { SetProperty(ref _listOfPOI, value); } }

        private ObservableCollection<Entry> _listOfEntries;
        public ObservableCollection<Entry> ListOfEntries { get => _listOfEntries; set { SetProperty(ref _listOfEntries, value); } }

        private ObservableCollection<Comment> _listOfComments;
        public ObservableCollection<Comment> ListOfComments { get => _listOfComments; set { SetProperty(ref _listOfComments, value); } }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }
        private ObservableCollection<Category> _groupedPOIList;
        public ObservableCollection<Category> GroupedPOIList
        {
            get { return _groupedPOIList; }
            set { SetProperty(ref _groupedPOIList, value); }
        }
        #endregion;


        //Methods
        #region Methods
        public async override Task InitAsync()
        {
            //await CenterOnUser();
            SwitchWeatherTxt = "Visa väder";
        }

        public async Task StandardMapView()
        {
            MapPage.CustomMap.MapType = MapType.Street;
        }

        public async Task SatelliteMapView()
        {
            MapPage.CustomMap.MapType = MapType.Satellite;
        }

        public async Task HybridMapView()
        {
            MapPage.CustomMap.MapType = MapType.Hybrid;
        }

        async Task<bool> AddPOI_Clicked()
        {
            if (!string.IsNullOrEmpty(poiToAdd.Name) && _entryRating > 0 && !string.IsNullOrEmpty(entryToAdd.Description))
            {
                await PopulateEntry();
                if (SelectedCategory == null)
                {
                    await App.Current.MainPage.DisplayAlert("Felmeddelande", "Du måste välja en kategori", "OK");
                    return false;
                }
                poiToAdd.Category = SelectedCategory.Name;
                try
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(ListOfPins.Last().Position.Latitude, ListOfPins.Last().Position.Longitude);
                    var placemark = placemarks.FirstOrDefault();
                    var city = placemark.Locality;
                    var country = placemark.CountryName;
                    var address = placemark.FeatureName;

                    ListOfPins.Last().Address = address;
                    ListOfPins.Last().Label = poiToAdd.Name;

                    await _entryClient.Post("entry", entryToAdd);
                    await GetPOIList(poiToAdd.Country, poiToAdd.City);
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                    MapPage.CustomMap.Pins.Remove(MapPage.CustomMap.Pins.Last());
                }
                if (addEntryModalIsVisible)
                {
                    try
                    {
                        addEntryModalIsVisible = false;
                        await GetPOIList(CountryName, CityName);
                        SelectedPOI = ListOfPOI.Data.FirstOrDefault(x => x.Name == CountryName && x.City == CityName);
                        ListOfEntries = Entries;
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Du har redan lagt en kommentar på den här sevärdheten!", "", "OK");

                    }
                }
                addPoiModalIsVisible = false;
                return true;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fel", "Du måste fylla i samtliga fält för att kunna lägga till en sevärdhet!", "OK");
            }
            return false;
        }

        private ObservableCollection<WeatherMinMax> _weatherMinMax;
        public ObservableCollection<WeatherMinMax> WeatherMinMax
        {
            get { return _weatherMinMax; }
            set { SetProperty(ref _weatherMinMax, value); }
        }
        private async Task SearchButton_Clicked()
        {
            MapPage.CustomMap.IsShowingUser = false;
            AvgRating = null;
            TitleResult = SearchBarText[0].ToString().ToUpper() + SearchBarText.Substring(1);
            POIListIsVisible = true;
            if (_titleResult == "Location")
            {
                return;
            }

            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(SearchBarText);
            if (approximateLocations.Count() == 0)
            {
                await App.Current.MainPage.DisplayAlert("Felmeddelande", "Kunde inte hitta platsen du sökte på", "OK");
                return;
            }

            Position position = approximateLocations.FirstOrDefault();
            string coordinates = $"{position.Latitude}, {position.Longitude}";

            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks.FirstOrDefault();

            var response = await weatherClient.Get("forecast", $"?longitude={position.Longitude}&latitude={position.Latitude}");

            TitleResult = placemark.Locality;

            var grouping = response.Data.WeatherList.GroupBy(x => x.DateTime.DayOfWeek);
            WeatherMinMax = new ObservableCollection<WeatherMinMax>();

            var culture = new CultureInfo("sv-SE");
            foreach (var group in grouping)
            {
                WeatherMinMax
                    .Add(new Models.WeatherMinMax
                    {
                        Day = culture.DateTimeFormat.GetDayName(group.Key)[0].ToString().ToUpper() + culture.DateTimeFormat.GetDayName(group.Key).Substring(1),
                        TempMax = group.Max(x => x.Temperature),
                        TempMin = group.Min(x => x.Temperature)
                    });
            }

            WeatherListIsVisible = false;

            CurrentWeather = (int)Math.Round(response.Data.WeatherList.FirstOrDefault().Temperature);
            MapSpan maps = new MapSpan(position, 1.10, 0.10);
            MapPage.CustomMap.MoveToRegion(maps);

            var test = await GetPOIList(placemark.CountryName, placemark.Locality);

            var poiGroup = test.Data.GroupBy(x => x.Category).ToDictionary(x => x.Key, x => x.ToList());

            GroupedPOIList = new ObservableCollection<Category>();
            foreach (var group in poiGroup)
            {
                GroupedPOIList.Add(new Category(group.Key, group.Value));
            }

            PinStay(test);
        }

        private async Task PinIcon_Clicked()
        {
            if (UserOffLine.LoggedIn)
            {
                pinner = new Pin()
                {
                    Label = "",
                    Address = "",
                    Type = PinType.Place
                };
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Offline", "Du måste vara en användare för att kunna lägga en pin", "Ok");
            }
        }
        public static bool deletepin { get; set; }
        public async Task<bool> MapClicked(object sender, MapClickedEventArgs e)
        {
            if (pinner != null)
            {
                pinner.Position = e.Position;
                MapPage.CustomMap.Pins.Add(pinner);

                var ans = await App.Current.MainPage.DisplayAlert("Hej", "Vill du lägga till en pin?", "Ja", "Nej");

                if (ans != true)
                {
                    MapPage.CustomMap.Pins.Remove(pinner);
                    pinner = null;
                }
                else
                {
                    MPVM.addPoiModalIsVisible = true;
                    await PopulatePOI(e.Position);
                    ListOfPins.Add(pinner);

                    pinner = null;
                    return true;
                }
            }
            if (addEntryModalIsVisible || addPoiModalIsVisible || CommentOnEntryModalIsVisible)
            {
                if (addPoiModalIsVisible)
                {
                    pinner = ListOfPins.Last();
                    MapPage.CustomMap.Pins.Remove(pinner);
                    ListOfPins.Remove(pinner);
                    pinner = null;
                    deletepin = true;
                }
                MPVM.addEntryModalIsVisible = false;
                MPVM.addPoiModalIsVisible = false;
                MPVM.CommentOnEntryModalIsVisible = false;

                poiToAdd.Name = "";
            }
            return false;
        }

        public void RatingAmount(object sender)
        {
            EntryRating = int.Parse(sender.ToString());
        }

        public async Task PopulatePOI(Position position)
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks.FirstOrDefault();

            poiToAdd.Country = placemark.CountryName; ;
            poiToAdd.Longitude = position.Longitude;
            poiToAdd.Latitude = position.Latitude;
            poiToAdd.City = placemark.Locality;
        }

        public async Task PopulateEntry()
        {
            entryToAdd.POI = poiToAdd;
            entryToAdd.Rating = EntryRating;
        }

        public async Task<PaginationResponse<ObservableCollection<POI>>> GetPOIList(string country, string city)
        {
            return await poiListClient.GetList("poi/list", $"?Country={country}&City={city}&amount=50");
        }

        public async Task PinStay(PaginationResponse<ObservableCollection<POI>> ListOfPOI)
        {
            MapPage.CustomMap.Pins.Clear();

            foreach (var item in ListOfPOI.Data)
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(item.Latitude, item.Longitude);
                var placemark = placemarks.FirstOrDefault();
                var city = placemark.Locality;
                var country = placemark.CountryName;
                var address = placemark.FeatureName;

                pinner = new Pin
                {
                    Position = new Position(item.Latitude, item.Longitude),
                    Address = address,
                    Label = item.Name,
                };
                ListOfPins.Add(pinner);
            }
            pinner = null;
            OnShowPinsEventHandler(ListOfPOI);
        }

        private async Task<ObservableCollection<Entry>> ShowEntriesForPOI()
        {
            Response<User> currentUser = null;
            if (UserOffLine.LoggedIn)
            {
                currentUser = await userClient.Get(@"user", $"?ApiKey={UserApi.ApiKey}");
                if (SelectedPOI != null)
                {
                    POIListIsVisible = false;
                    AvgRating = SelectedPOI.AvgRating.ToString();
                    EntryListIsVisible = true;
                    BackArrowIsVisible = true;
                    FoldButtonIsVisible = false;
                    if (SelectedPOI.Entries.FirstOrDefault(x => x.Username == currentUser.Data.Username) != null)
                    {
                        EntryButtonIsVisible = false;
                    }
                    else
                    {
                        EntryButtonIsVisible = true;
                    }
                    currentUser = null;
                    poiToAdd.Name = SelectedPOI.Name;
                    TitleResult = SelectedPOI.Name;

                    EntryPagination = await _entryClient.GetList("entry", $"?amount=6&poi={SelectedPOI.Name.Replace(" ", "+")}");
                    PreviousEntriesVisible = true;
                    NextEntriesVisible = true;
                    if (EntryPagination.PrevPage == null)
                    {
                        PreviousEntriesVisible = false;
                    }
                    if (EntryPagination.NextPage == null)
                    {
                        NextEntriesVisible = false;
                    }
                    ListOfEntries = EntryPagination.Data;
                    ShowUserLikes();
                    CityName = SelectedPOI.City;
                    POIName = SelectedPOI.Name;
                    Latitude = SelectedPOI.Latitude;
                    Longitude = SelectedPOI.Longitude;
                    Entries = SelectedPOI.Entries;
                    SelectedPOI = null;
                    return ListOfEntries;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Offline", "Du måste vara en användare för att kunna se inlägg", "Ok");
            }
            return null;
        }

        private async Task ShowUserLikes()
        {
            var currentUser = await userClient.Get(@"user", $"?ApiKey={UserApi.ApiKey}");
            var listOfLikesFromUser = ListOfEntries.SelectMany(x => x.LikeDislikeEntries).Where(x => x.UserId == currentUser.Data.Id).ToList();
            if (listOfLikesFromUser.Count != 0)
            {
                foreach (var like in listOfLikesFromUser)
                {
                    ListOfEntries.First(x => x.LikeDislikeEntries.Contains(like)).LikeButtonImageSource = @"File:.\Assets\LikeButtonFilled.png";
                }
            }
        }

        private async Task BackArrowClicked()
        {
            if (EntryListIsVisible == false)
            {
                CommentListIsVisible = false;
                EntryListIsVisible = true;
                TitleResult = POIName;
            }
            else
            {
                EntryListIsVisible = false;
                POIListIsVisible = true;
                BackArrowIsVisible = false;
                FoldButtonIsVisible = true;
                TitleResult = CityName;
                AvgRating = null;
            }
        }

        private async Task FoldFrameClicked(Frame sender)
        {
            if (sender.IsVisible)
            {
                await sender.FadeTo(0, 500, Easing.SpringOut);
                FoldButtonIsVisible = false;
                FoldInFrameIsVisible = true;
                sender.IsVisible = false;
            }
            else if (!sender.IsVisible)
            {
                FoldButtonIsVisible = true;
                FoldInFrameIsVisible = false;
                sender.IsVisible = true;
                await sender.FadeTo(1, 500, Easing.SpringIn);
            }
        }
        private async Task EntryButton_Clicked()
        {
            addEntryModalIsVisible = true;
            Position position = new Position(Latitude, Longitude);
            await PopulatePOI(position);
        }
        private async Task LikeButton_Clicked(object sender)
        {
            var selectedEntry = sender as Entry;
            try
            {
                await httpClientLike.Post($"entry/like/{selectedEntry.Id}");

                if (selectedEntry.LikeButtonImageSource == @"File:.\Assets\LikeButtonFilled.png")
                {
                    selectedEntry.LikeButtonImageSource = @"File:.\Assets\LikeButtonNotFilled.png";
                }
                else
                {
                    selectedEntry.LikeButtonImageSource = @"File:.\Assets\LikeButtonFilled.png";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task CenterOnUser()
        {
            MapPage.CustomMap.IsShowingUser = true;
            var position = await Geolocation.GetLocationAsync();
            var response = await weatherClient.Get("forecast", $"?longitude={position.Longitude}&latitude={position.Latitude}");
            CurrentWeather = (int)Math.Round(response.Data.WeatherList.FirstOrDefault().Temperature);
           
            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks.FirstOrDefault();
            TitleResult = placemark.Locality; 
            var test = await GetPOIList(placemark.CountryName, placemark.Locality);

            var poiGroup = test.Data.GroupBy(x => x.Category).ToDictionary(x => x.Key, x => x.ToList());

            GroupedPOIList = new ObservableCollection<Category>();
            foreach (var group in poiGroup)
            {
                GroupedPOIList.Add(new Category(group.Key, group.Value));
            }
        }

        private void SwitchWeatherEntry()
        {
            WeatherListIsVisible = POIListIsVisible ? true : false;
            POIListIsVisible = !POIListIsVisible;

            if (WeatherListIsVisible)
            {
                SwitchWeatherTxt = "Visa inlägg";
            }
            else
            {
                SwitchWeatherTxt = "Visa väder";
            }
        }
        

        private async Task OnGetPreviousEntries()
        {
            EntryPagination = await _entryClient.GetList("entry", "?" + EntryPagination.PrevPage.Split('?')[1]);
            ListOfEntries = EntryPagination.Data;
            PreviousEntriesVisible = true;
            NextEntriesVisible = true;
            if (EntryPagination.PrevPage == null)
            {
                PreviousEntriesVisible = false;
            }
            if (EntryPagination.NextPage == null)
            {
                NextEntriesVisible = false;
            }
        }
        private async Task OnGetNextEntries()
        {
            EntryPagination = await _entryClient.GetList("entry", "?" + EntryPagination.NextPage.Split('?')[1]);
            ListOfEntries = EntryPagination.Data;
            PreviousEntriesVisible = true;
            NextEntriesVisible = true;
            if (EntryPagination.PrevPage == null)
            {
                PreviousEntriesVisible = false;
            }
            if (EntryPagination.NextPage == null)
            {
                NextEntriesVisible = false;
            }
        }
        private async Task OnPOISelected(object param)
        {
            SelectedPOI = param as POI;
        }

        private async Task OnCommentButtonClicked(object sender)
        {
            SelectedEntry = sender as Entry;

            CommentListIsVisible = true;
            EntryListIsVisible = false;
            TitleResult = SelectedEntry.Description;
            ListOfComments = SelectedEntry.Comments;
        }

        private async Task OpenEntryComment()
        {
            CommentOnEntryModalIsVisible = true;
        }

        private async Task PostComment()
        {
            var commentToPost = new Comment();
            commentToPost.Text = CommentOnEntry;
            commentToPost.EntryId = SelectedEntry.Id;
            var currentUser = await userClient.Get(@"user", $"?ApiKey={UserApi.ApiKey}");
            commentToPost.UserId = currentUser.Data.Id;
            await commentClient.Post("comments", commentToPost);
            CommentOnEntryModalIsVisible = false;
        }
        #endregion;
    }
}
