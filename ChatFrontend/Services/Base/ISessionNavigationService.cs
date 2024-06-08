using ShopContent.ViewModels.Base;
using System;

namespace ChatFrontend.Services.Base
{
    public interface ISessionNavigationService
    {
        public ViewModel CurrentView { get; }

        public Func<Type, ViewModel> ViewModelFactory { get; set; }

        public void NavigateTo<T>() where T : ViewModel;

        void Logout();
    }
}
