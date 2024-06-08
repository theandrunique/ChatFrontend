using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;
using System;

namespace ChatFrontend.Services
{
    public class SessionNavigationService : ViewModel, ISessionNavigationService
    {
        private Func<Type, ViewModel> _viewModelFactory;
        private ViewModel _currentView;

        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public Func<Type, ViewModel> ViewModelFactory
        {
            get => _viewModelFactory;
            set
            {
                _viewModelFactory = value;
                OnPropertyChanged(nameof(ViewModelFactory));
            }
        }

        public void NavigateTo<T>() where T : ViewModel
        {
            var viewModel = _viewModelFactory.Invoke(typeof(T));
            CurrentView = viewModel;
        }

        public void Logout()
        {
            CurrentView = null;
            ViewModelFactory = null;
        }
    }
}
