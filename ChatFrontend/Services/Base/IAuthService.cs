using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    interface IAuthService
    {
        Task<string> Login(string login, string password);
        Task<bool> SignUp(string username, string email, string password);
    }
}
