using Newtonsoft.Json;

namespace ChatFrontend.Classes
{
    public class Chat
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("messages_count")]
        public int MessagesCount;
        [JsonProperty("last_message")]
        public Message LastMessage;
        [JsonProperty("member_id")]
        public string MemberId;
    }
}
