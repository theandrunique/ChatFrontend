using ChatFrontend.Models;
using ChatFrontend.Services.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IMessengerService
    {
        Task<List<Chat>> GetChats();

        Task<MessagesResponse> GetMessages(string chatId, int count, int offset);
        Task<MessagesResponse> GetPrivateMessages(string userId, int count, int offset);

        Task<NewMessageResponse> SendMessage(string chatId, string message);
        Task<NewMessageResponse> SendPrivateMessage(string userId, string message);
    }
}
