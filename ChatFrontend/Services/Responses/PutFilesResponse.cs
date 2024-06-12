using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChatFrontend.Services.Responses
{
    public class PutFilesResponse
    {
        [JsonProperty("files")]
        public Dictionary<string, string> Files { get; set; }
    }
}
