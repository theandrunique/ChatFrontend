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
        readonly IJsonService converter = new JsonService();

        public AuthService()
        {
            client.BaseAddress = new Uri(AuthServiceSettings.BaseUrl);
        }
        public async Task<string> Login(string login, string password)
        {
            var json = new
            {
                login = login,
                password = password,
            };
            var response = await client.PostAsync("/auth/token", Helper.CreateJsonContent(json));
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode >= 400)
            {
                if (statusCode == 422)
                    throw await Helper.HandleUnprocessableEntity(response);
                throw await Helper.HandleCommonError(response);
            }
            var tokenResponse = converter.Deserialize<TokenResponse>(await response.Content.ReadAsStringAsync());
            return tokenResponse.Token;
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
    }
}
