using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class StorageService : IStorageService
    {
        private readonly HttpClient _client;
        private readonly IJsonService _jsonService;

        public StorageService(IJsonService jsonService)
        {
            _jsonService = jsonService;
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8010"),
            };
        }

        public async Task<PutFilesResponse> PutFiles(FileStream file)
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            content.Add(streamContent, "files", file.Name);

            var response = await _client.PostAsync($"/file/upload", content);

            var filesResponse = _jsonService.Deserialize<PutFilesResponse>(await response.Content.ReadAsStringAsync());
            return filesResponse;
        }
    }
}
