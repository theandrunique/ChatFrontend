using ChatFrontend.Models;
using ChatFrontend.Services.Base;

namespace ChatFrontend.Core
{
    public class AppState
    {
        public static AppState Instance;

        private readonly INavigationService _navigationService;

        private User _user;

        public User User { get => _user; set => _user = value; }

        public AppState(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Instance = this;
        }
    }
}
