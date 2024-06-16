using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _client = new HttpClient();
        readonly Settings _settings;

        private TokenResponse _tokenResponse;
        private DateTime _tokenObtainedAt;

        public AuthService(Settings settings)
        {
            _settings = settings;
            _client.BaseAddress = new Uri(settings.AuthServiceBaseUrl);
        }

        public async Task<string> GetAccessToken()
        {
            if (TokenExpired())
            {
                await RefreshToken();
            }
            return _tokenResponse.AccessToken;
        }

        public async Task<User> GetMe()
        {
            var response = await _client.GetAsync("/users/me");
            return await Helper.HandleResponse<User>(response);
        }

        public async Task<User> UpdateImageUrl(string url)
        {
            var json = new
            {
                image_url = url,
            };

            var response = await _client.PutAsync("/users/me/image_url", Helper.CreateJsonContent(json));

            return await Helper.HandleResponse<User>(response);
        }

        public async Task<bool> Login(string login, string password)
        {
            var json = new
            {
                login = login,
                password = password,
            };
            var response = await _client.PostAsync("/auth/sign-in", Helper.CreateJsonContent(json));
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

            var response = await _client.PostAsync("/auth/sign-up", Helper.CreateJsonContent(json));
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }
            return statusCode == 201;
        }

        private bool TokenExpired()
        {
            if (_tokenResponse == null)
                return true;

            var expirationTime = _tokenObtainedAt.AddSeconds(_tokenResponse.ExpiresIn);
            return DateTime.UtcNow >= expirationTime;
        }

        private async Task RefreshToken()
        {
            if (_tokenResponse == null)
            {
                await RequestNewToken();
            }
            else
            {
                var tokenRequestUri = "/oauth/token";
                var clientId = _settings.AuthServiceClientId;
                var clientSecret = _settings.AuthServiceClientSecret;

                var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("refresh_token", _tokenResponse.RefreshToken),
                    new KeyValuePair<string, string>("grant_type", "refresh_token")
                });

                var response = await _client.PostAsync(tokenRequestUri, content);
                _tokenResponse = await Helper.HandleResponse<TokenResponse>(response);
                _tokenObtainedAt = DateTime.UtcNow;
            }
        }

        private async Task RequestNewToken()
        {
            var requestUri = $"/oauth/authorize?client_id={_settings.AuthServiceClientId}&redirect_uri={_settings.AuthServiceRedirectUri}&response_type=web_message";
            var response = await _client.PostAsync(requestUri, null);

            var codeResponse = await Helper.HandleResponse<AuthorizationCodeResponse>(response);
            var authorizationCode = codeResponse.Code;

            var tokenRequestUri = "/oauth/token";
            var clientId = _settings.AuthServiceClientId;
            var clientSecret = _settings.AuthServiceClientSecret;
            var redirectUri = _settings.AuthServiceRedirectUri;

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

            var tokenResponseMessage = await _client.PostAsync(tokenRequestUri, content);
            _tokenResponse = await Helper.HandleResponse<TokenResponse>(tokenResponseMessage);
            _tokenObtainedAt = DateTime.UtcNow;
        }
    }
}
