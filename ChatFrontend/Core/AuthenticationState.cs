using ChatFrontend.Models;

namespace ChatFrontend.Core
{
    public class AuthenticationState
    {
        private bool _isAuthenticated;
        private string _accessToken;
        private User _user;

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
            set
            {
                _isAuthenticated = value;
            }
        }

        public string AccessToken { get => _accessToken; set => _accessToken = value; }
        public User User { get => _user; set => _user = value; }
    }
}
