using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
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
    }
}
