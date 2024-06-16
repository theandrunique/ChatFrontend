using ChatFrontend.Models;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IAuthService
    {
        Task<string> GetAccessToken();
        Task<User> GetMe();
        Task<bool> Login(string login, string password);
        Task<bool> SignUp(string username, string email, string password);
        Task<User> UpdateImageUrl(string imageUrl);
    }
}
