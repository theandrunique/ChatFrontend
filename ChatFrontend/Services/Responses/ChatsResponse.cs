using ChatFrontend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChatFrontend.Services.Responses
{
    public class ChatsResponse
    {
        [JsonProperty("chats")]
        public List<Chat> Chats { get; set; }
    }
}
