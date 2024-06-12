using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace ChatFrontend.Models
{
    public class Chat
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("creator_id")]
        public string CreatorId { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("members")]
        public List<string> Members { get; set; }

        [JsonProperty("admins")]
        public List<string> Admins { get; set; }

        [JsonProperty("message_count")]
        public int MessageCount { get; set; }

        [JsonProperty("last_message")]
        public Message LastMessage { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
