using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class NavigationMenuVM : ViewModel
    {
        private INavigationService _navigation;
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateToMessengerCommand { get; }
        public NavigationMenuVM(INavigationService navigation)
        {
            _navigation = navigation;
            NavigateToLoginCommand = new LambdaCommand((o) => _navigation.NavigateTo<LoginVM>());
            NavigateToMessengerCommand = new LambdaCommand((o) => _navigation.NavigateTo<MessengerVM>());
        }
    }
}
