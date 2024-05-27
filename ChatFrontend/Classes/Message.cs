using System;
using Newtonsoft.Json;

namespace ChatFrontend.Classes
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("from_id")]
        public string FromId;
        [JsonProperty("sended_at")]
        public DateTime SendedAt;
        [JsonProperty("message_id")]
        public int Id;
    }
}
