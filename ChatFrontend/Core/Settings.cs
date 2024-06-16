namespace ChatFrontend.Core
{
    public sealed class Settings
    {
        public string AuthServiceBaseUrl { get; set; }
        public string MessengerServiceBaseUrl { get; set; }
        public string MessengerWebSocketUrl { get; set; }
        public string AuthServiceClientId { get; set; }
        public string AuthServiceClientSecret { get; set; }
        public string AuthServiceRedirectUri { get; set; }
    }
}
