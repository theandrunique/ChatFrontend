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
    public class UsersService : IUsersService
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly IAuthService _authService;

        public UsersService(Settings settings, IAuthService authService)
        {
            _authService = authService;
            _client.BaseAddress = new Uri(settings.AuthServiceBaseUrl);
        }

        public async Task<User> GetMe()
        {
            return await _authService.GetMe();
        }

        public async Task<User> GetUserById(string id)
        {
            var response = await _client.GetAsync($"/users/{id}");
            return await Helper.HandleResponse<User>(response);
        }

        public async Task<UsersSearchResponse> SearchUsers(string query)
        {
            var response = await _client.GetAsync($"/users/search?query={query}");
            return await Helper.HandleResponse<UsersSearchResponse>(response);
        }

        public async Task<UsersSearchResponse> GetUsers(List<string> userIds)
        {
            var response = await _client.GetAsync($"/users?user_ids={string.Join("&user_ids=", userIds)}");
            return await Helper.HandleResponse<UsersSearchResponse>(response);
        }
    }
}
