using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChatFrontend.Classes
{
    public class AvatarsAdapter
    {
        public static string BaseUrl = "http://localhost:8000";
        public async static Task<BitmapImage> GetAvatar(string userId)
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync($"{BaseUrl}/f/{userId}");
                using (var stream = await res.Content.ReadAsStreamAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();

                        return bitmapImage;
                    }
                }
            }
        }
        public static string GetAvatarUrl(string userId)
        {
            return $"{BaseUrl}/f/{userId}";
        }
        public async static Task<HttpResponseMessage> UpdateProfileImage(FileStream file, string userId)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file), "file", "photo.jpg");

                return await client.PostAsync($"{BaseUrl}/f/uploadfile/{userId}", content);
            }

        }
    }
}
