using ChatFrontend.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChatFrontend.Services.Responses
{
    public class UsersSearchResponse
    {
        [JsonProperty("result")]
        public List<User> Result { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
