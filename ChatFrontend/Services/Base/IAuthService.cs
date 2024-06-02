using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IAuthService
    {
        Task<bool> Login(string login, string password);
        Task<bool> SignUp(string username, string email, string password);
        Task<string> Token();
    }
}
