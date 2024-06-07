using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatFrontend.Models
{
    public class Message
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("from_id")]
        public string FromId { get; set; }

        [JsonProperty("attachments")]
        public List<string> Attachments { get; set; }

        public string ImageUrl
        {
            get => "https://avatars.githubusercontent.com/u/127850940?s=400&u=52b76119c01837beeac91a9840bf76e00b805296&v=4";
        }
    }
}
