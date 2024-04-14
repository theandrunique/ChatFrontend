using ChatFrontend.Classes.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ChatFrontend.Classes
{
    public class BackendAdapter
    {
        public static string BaseUrl = "http://localhost:8001/";
        private HttpClient client;
        private string AuthHeader = null;
        public BackendAdapter()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
        }
        public async Task<HttpResponseMessage> GetAuth(string login, string password)
        {
            var json = new
            {
                login = login,
                password = password,
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(json), 
                Encoding.UTF8, 
                "application/json"
            );
            return await client.PostAsync("/auth/login/", content);
        }
        public async Task<HttpResponseMessage> SignUp(string username, string email, string password)
        {
            var json = new
            {
                username = username,
                email = email,
                password = password,
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8,
                "application/json"
            );
            return await client.PostAsync("/auth/register/", content);
        }
        public static async Task<string> HandleError(HttpResponseMessage response)
        {
            int statusCode = Convert.ToInt32(response.StatusCode);
            if (statusCode == 422)
            {
                var json = await response.Content.ReadAsStringAsync();
                UnprocessableEntity error = JsonConvert.DeserializeObject<UnprocessableEntity>(json);

                StringBuilder errorMessage = new StringBuilder();
                foreach (var detail in error.Detail)
                {
                    errorMessage.AppendLine($"{detail.Loc[1]} | {detail.Msg}");
                }
                return errorMessage.ToString();
            }
            else if (statusCode >= 400 && statusCode < 500)
            {
                var json = await response.Content.ReadAsStringAsync();

                ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(json);

                return error.Detail;
            }
            else if (statusCode >= 500 && statusCode < 600)
            {
                return "Sever Error";
            }
            throw new ArgumentException("Response is not an error");
        }
        public void AddAuthHeader(string token)
        {
            AuthHeader = $"Bearer {token}";
        }
        public async Task<HttpResponseMessage> ConfirmElailSend(string email)
        {
            var json = new
            {
                email = email,
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(json),
                Encoding.UTF8,
                "application/json"
            );
            return await client.PostAsync("/emails/verify/", content);
        }
    }
}
