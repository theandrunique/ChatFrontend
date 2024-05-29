using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels.Auth;
using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        public MainWindowVM(INavigationService navigation)
        {
            Navigation = navigation;
            Navigation.NavigateTo<LoginVM>();
        }
    }
}
