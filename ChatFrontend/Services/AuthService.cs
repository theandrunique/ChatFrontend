using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient client = new HttpClient();
        readonly JsonService jsonService = new JsonService();

        public AuthService()
        {
            client.BaseAddress = new Uri(AuthServiceSettings.BaseUrl);
        }

        public async Task<User> GetMe()
        {
            var response = await client.GetAsync("/users/me");
            int statusCode = Convert.ToInt32(response.StatusCode);
            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }

            var user = jsonService.Deserialize<User>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var response = await client.GetAsync($"/users/{id}");
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode == 404)
                return null;

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }

            var user = jsonService.Deserialize<User>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task<UsersSearchResponse> SearchUsers(string query)
        {
            var response = await client.GetAsync($"/users/search?query={query}");
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode == 404)
                return null;

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }

            var searchResult = jsonService.Deserialize<UsersSearchResponse>(await response.Content.ReadAsStringAsync());
            return searchResult;
        }

        public async Task<bool> Login(string login, string password)
        {
            var json = new
            {
                login = login,
                password = password,
            };
            var response = await client.PostAsync("/auth/login", Helper.CreateJsonContent(json));
            int statusCode = Convert.ToInt32(response.StatusCode);
            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SignUp(string username, string email, string password)
        {
            var json = new
            {
                username = username,
                email = email,
                password = password,
            };

            var response = await client.PostAsync("/auth/signup", Helper.CreateJsonContent(json));
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }
            return statusCode == 201;
        }

        public async Task<string> Token()
        {
            var clientId = "5353554b-557e-4872-976e-baf669b2c708";
            var redirectUri = "http://example.com";
            var responseType = "token";

            var requestUri = $"/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&response_type={responseType}";

            var response = await client.PostAsync(requestUri, null);

            int statusCode = Convert.ToInt32(response.StatusCode);
            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }
            var token = jsonService.Deserialize<TokenResponse>(await response.Content.ReadAsStringAsync());
            return token.AccessToken;
        }

        public async Task<UsersSearchResponse> GetUsers(List<string> userIds)
        {
            var response = await client.GetAsync($"/users?user_ids={string.Join("&user_ids=", userIds)}");
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode == 404)
                return null;

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }

            var searchResult = jsonService.Deserialize<UsersSearchResponse>(await response.Content.ReadAsStringAsync());
            return searchResult;
        }
    }
}
