using ChatFrontend.Models;
using Newtonsoft.Json;

namespace ChatFrontend.Services.Responses
{
    public class NewMessageResponse
    {
        [JsonProperty("chat")]
        public Chat Chat { get; set; }
        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}
