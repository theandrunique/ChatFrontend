using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class MessengerService : IMessengerService
    {
        readonly HttpClient client = new HttpClient();
        readonly IJsonService jsonService = new JsonService();

        public MessengerService(AuthenticationState authenticationState)
        {
            if (authenticationState.IsAuthenticated == false)
                throw new Exception("Can't use messenger service without authentication");

            client.BaseAddress = new Uri(MessengerServiceSettings.BaseUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticationState.AccessToken}");
        }

        public async Task<List<Chat>> GetChats()
        {
            var response = await client.GetAsync("/chats");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var chatsResponse = jsonService.Deserialize<ChatsResponse>(responseContent);
            return chatsResponse.Chats;
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
