using System.Windows.Input;
using Xamarin.Forms;

namespace NikeClientApp.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ICommand SetAppThemeCommand { get; set; }
        public SettingsViewModel(INaviService naviService) : base(naviService)
        {
            SetAppThemeCommand = new Command<SettingsViewModel>((x) => SetAppTheme());
        }

        void SetAppTheme()
        {

            if (App.Current.UserAppTheme == OSAppTheme.Dark)
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }
    }
}
