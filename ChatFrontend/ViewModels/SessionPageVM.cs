using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class SessionPageVM : ViewModel
    {
        private readonly ISessionNavigationService _sessionNavigation;
        private readonly INavigationService _navigation;

        public ISessionNavigationService SessionNavigation { get => _sessionNavigation; }

        public ICommand LogoutCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateToMessengerCommand { get; }
        public ICommand NavigateToSearchCommand { get; }

        public SessionPageVM(ISessionNavigationService sessionNavigationService, ISessionServiceProvider sessionServiceProvider, INavigationService navigationService)
        {
            _navigation = navigationService;

            _sessionNavigation = sessionNavigationService;
            _sessionNavigation.ViewModelFactory = sessionServiceProvider.GetViewModelFactory();

            NavigateToSettingsCommand = new LambdaCommand((o) => _sessionNavigation.NavigateTo<SettingsVM>());
            NavigateToMessengerCommand = new LambdaCommand((o) => _sessionNavigation.NavigateTo<MessengerVM>());
            NavigateToSearchCommand = new LambdaCommand((o) => _sessionNavigation.NavigateTo<SearchVM>());

            LogoutCommand = new LambdaCommand(ExecuteLogoutCommand);

            _sessionNavigation.NavigateTo<MessengerVM>();
        }

        private void ExecuteLogoutCommand(object obj)
        {
            _sessionNavigation.Logout();

            _navigation.NavigateTo<LoginVM>();
        }
    }
}
