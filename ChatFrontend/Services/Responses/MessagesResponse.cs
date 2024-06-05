using ChatFrontend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChatFrontend.Services.Responses
{
    public class MessagesResponse
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
        [JsonProperty("chat")]
        public Chat Chat { get; set; }
    }
}
