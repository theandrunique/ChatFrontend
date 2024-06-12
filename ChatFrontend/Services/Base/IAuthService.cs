using ChatFrontend.Models;
using ChatFrontend.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ChatFrontend.Services.Base
{
    public interface IAuthService
    {
        Task<bool> Login(string login, string password);
        Task<bool> SignUp(string username, string email, string password);
        Task<string> Token();
        Task<User> GetMe();
        Task<User> GetUserById(string id);
        Task<UsersSearchResponse> SearchUsers(string query);
        Task<UsersSearchResponse> GetUsers(List<string> userIds);
    }
}
