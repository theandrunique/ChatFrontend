using ChatFrontend.Models;
using ChatFrontend.Services.Base;

namespace ChatFrontend.Core
{
    public class AppState
    {
        public static AppState Instance;

        private readonly INavigationService _navigationService;

        private bool _isAuthenticated;
        private string _accessToken;
        private User _user;

        public bool IsAuthenticated { get => _isAuthenticated; private set => _isAuthenticated = value; }
        public string AccessToken { get => _accessToken; private set => _accessToken = value; }
        public User User { get => _user; private set => _user = value; }

        public AppState(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Instance = this;
        }

        public void Login(string accessToken, User user)
        {
            AccessToken = accessToken;
            User = user;
            IsAuthenticated = true;
        }

        public void Logout()
        {
            AccessToken = null;
            User = null;
            IsAuthenticated = false;
        }
    }
}
