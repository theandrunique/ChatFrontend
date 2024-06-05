using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ChatFrontend.Services
{
    public class MessengerService : IMessengerService
    {
        readonly HttpClient client = new HttpClient();
        readonly IJsonService jsonService = new JsonService();
        WebSocket webSocket;

        public event Action<NewMessageResponse> OnMessageReceived;

        public MessengerService()
        {
            client.BaseAddress = new Uri(MessengerServiceSettings.BaseUrl);
        }

        public void SetToken(string token)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            InitializeWebSocket(token);
        }

        public void RemoveToken()
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            if (webSocket != null && webSocket.IsAlive)
            {
                webSocket.Close();
            }
        }

        private void InitializeWebSocket(string token)
        {
            webSocket = new WebSocket($"{MessengerServiceSettings.WebSocketUrl}?access_token={token}");

            webSocket.OnMessage += (sender, e) =>
            {
                ReceiveMessage(e.Data);
            };
            webSocket.Connect();
        }

        private void ReceiveMessage(string message)
        {
            var newMessage = jsonService.Deserialize<NewMessageResponse>(message);
            OnMessageReceived?.Invoke(newMessage);
        }

        public async Task<ChatsResponse> GetChats()
        {
            var response = await client.GetAsync("/chats");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return jsonService.Deserialize<ChatsResponse>(responseContent);
        }

        public async Task<MessagesResponse> GetMessages(string chatId, int count, int offset)
        {
            var response = await client.GetAsync($"/chats/{chatId}/messages?count={count}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return jsonService.Deserialize<MessagesResponse>(responseContent);
        }

        public async Task<NewMessageResponse> SendMessage(string chatId, string message)
        {
            var sendMessageRequest = new { text = message };
            var content = Helper.CreateJsonContent(sendMessageRequest);

            var response = await client.PostAsync($"/chats/{chatId}/messages", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return jsonService.Deserialize<NewMessageResponse>(responseContent);
        }
    }
}
