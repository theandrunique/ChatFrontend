using Newtonsoft.Json;

namespace ChatFrontend.Services.Responses
{
    public class AuthorizationCodeResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
