
using Newtonsoft.Json;

namespace ChatFrontend.Classes.Responses
{
    public class AuthResponse
    {
        [JsonProperty("user_id")]
        public int UserId;
        [JsonProperty("token")]
        public string Token;
    }
}
