using Newtonsoft.Json;
using System.IO;

namespace ChatFrontend.Classes
{
    public class TokenCache
    {
        public string Token { get; set; }
    }
    public class Cache
    {
        public static readonly string CacheTokenPath = "cache.json";
        public static bool CheckTokenCache()
        {
            if (File.Exists(CacheTokenPath))
            {
                var cache = File.ReadAllText(CacheTokenPath);
                var tokenCache = JsonConvert.DeserializeObject<TokenCache>(cache);
                MainWindow.Adapter.AddAuthHeader(tokenCache.Token);
                return true;
            }
            return false;
        }
        public static void CacheToken(string token)
        {
            var json = JsonConvert.SerializeObject(new TokenCache { Token = token });
            File.WriteAllText(CacheTokenPath, json);
        }
    }
}
