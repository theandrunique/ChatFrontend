using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatFrontend.Classes.Responses
{
    public class MessagesResponse
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}
