using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatFrontend.Services
{
    public class Helper
    {
        readonly static IJsonService converter = new JsonService();

        public static StringContent CreateJsonContent(object obj)
        {
            return new StringContent(converter.Serialize(obj), Encoding.UTF8, "application/json");
        }
        public async static Task<UnprocessableEntity> HandleUnprocessableEntity(HttpResponseMessage response)
        {
            if (Convert.ToInt32(response.StatusCode) != 422)
                throw new Exception("Unexpected behavior");

            string json = await response.Content.ReadAsStringAsync();
            return converter.Deserialize<UnprocessableEntity>(json);
        }
        public async static Task<ErrorResponse> HandleCommonError(HttpResponseMessage response)
        {
            int statusCode = Convert.ToInt32(response.StatusCode);
            if (!(statusCode >= 400 && statusCode < 500))
                throw new Exception("Unexpected behavior");

            string json = await response.Content.ReadAsStringAsync();
            ErrorResponse error = converter.Deserialize<ErrorResponse>(json);
            return error;
        }
    }
}
