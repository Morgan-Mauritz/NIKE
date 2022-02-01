using NikeClientApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NikeClientApp.ViewModels
{
    public interface INaviService
    {
        bool CanGoBack { get; }

        Task GoBack();

        Task NavigateTo<TVM>() where TVM : BaseViewModel;

        Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;

        void RemoveLastView();

        void ClearBackStack();

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
