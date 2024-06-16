using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class MessengerService : IMessengerService
    {
        readonly HttpClient _client = new HttpClient();
        readonly IAuthService _authService;

        public MessengerService(IAuthService authService, Settings settings)
        {
            _authService = authService;
            _client.BaseAddress = new Uri(settings.MessengerServiceBaseUrl);
        }

        private async Task AddAuthorizationHeader()
        {
            var accessToken = await _authService.GetAccessToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<List<Chat>> GetChats()
        {
            await AddAuthorizationHeader();
            var response = await _client.GetAsync("/chats");
            var chats = await Helper.HandleResponse<ChatsResponse>(response);
            return chats.Chats;
        }

        public async Task<MessagesResponse> GetMessages(string chatId, int count, int offset)
        {
            await AddAuthorizationHeader();
            var response = await _client.GetAsync($"/chats/{chatId}/messages?count={count}&offset={offset}");
            return await Helper.HandleResponse<MessagesResponse>(response);
        }

        public async Task<MessagesResponse> GetPrivateMessages(string userId, int count, int offset)
        {
            await AddAuthorizationHeader();
            var response = await _client.GetAsync($"/users/{userId}/messages?count={count}&offset={offset}");
            return await Helper.HandleResponse<MessagesResponse>(response);
        }

        public async Task<NewMessageResponse> SendMessage(string chatId, string message)
        {
            await AddAuthorizationHeader();
            var sendMessageRequest = new { text = message };
            var content = Helper.CreateJsonContent(sendMessageRequest);

            var response = await _client.PostAsync($"/chats/{chatId}/messages", content);
            return await Helper.HandleResponse<NewMessageResponse>(response);
        }

        public async Task<NewMessageResponse> SendPrivateMessage(string userId, string message)
        {
            await AddAuthorizationHeader();
            var sendMessageRequest = new { text = message };
            var content = Helper.CreateJsonContent(sendMessageRequest);

            var response = await _client.PostAsync($"/users/{userId}/messages", content);
            return await Helper.HandleResponse<NewMessageResponse>(response);
        }
    }
}
