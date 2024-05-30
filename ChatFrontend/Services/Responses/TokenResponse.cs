using Newtonsoft.Json;

namespace ChatFrontend.Services.Responses
{
    public class TokenResponse
    {
        [JsonProperty("user_id")]
        public int UserId;
        [JsonProperty("token")]
        public string Token;
    }
}
