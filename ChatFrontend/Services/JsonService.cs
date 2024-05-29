using ChatFrontend.Services.Base;
using Newtonsoft.Json;

namespace ChatFrontend.Services
{
    public class JsonService : IJsonService
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
