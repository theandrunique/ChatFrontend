using ChatFrontend.Models;
using ChatFrontend.Services.Responses;
using System;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IMessengerService
    {
        event Action<NewMessageResponse> OnMessageReceived;

        Task<ChatsResponse> GetChats();
        Task<MessagesResponse> GetMessages(string chatId, int count, int offset);
        Task<NewMessageResponse> SendMessage(string chatId, string message);
        void SetToken(string token);
        void RemoveToken();
    }
}
