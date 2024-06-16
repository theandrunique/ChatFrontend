using Newtonsoft.Json;

namespace ChatFrontend.Services.Responses
{
    public class TokenResponse
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken;
        [JsonProperty("access_token")]
        public string AccessToken;
        [JsonProperty("expires_in")]
        public int ExpiresIn;
    }
}
