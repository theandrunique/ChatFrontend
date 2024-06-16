using ChatFrontend.Models;
using ChatFrontend.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IUsersService
    {
        Task<User> GetMe();
        Task<User> GetUserById(string id);
        Task<UsersSearchResponse> SearchUsers(string query);
        Task<UsersSearchResponse> GetUsers(List<string> userIds);
    }
}
