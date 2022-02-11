using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace NikeClientApp.ViewModels
{
    public class BaseViewModel : Map, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected INaviService NavigationService { get; private set; }

        protected BaseViewModel(INaviService naviService)
        {
            NavigationService = naviService;
        }
        public BaseViewModel()
        {

        }

        public virtual void Init()
        {
        }
        public async virtual Task InitAsync()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INaviService navService)
            : base(navService)
        {
        }

        public override void Init()
        {
            Init(default(TParameter));
        }

        public virtual void Init(TParameter parameter)
        {
        }
    }

}
