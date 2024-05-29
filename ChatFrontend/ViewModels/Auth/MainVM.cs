using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels.Auth
{
    public class MainVM : ViewModel
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

        public MainVM(INavigationService navigation)
        {
            Navigation = navigation;
            Navigation.NavigateTo<LoginVM>();
        }
    }
}
