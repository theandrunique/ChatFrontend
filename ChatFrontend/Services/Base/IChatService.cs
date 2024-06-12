using ChatFrontend.Models;
using ChatFrontend.Services.Responses;
using ChatFrontend.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IChatService
    {
        ObservableCollection<MessageVM> Messages { get; }

        ObservableCollection<ChatVM> Chats { get; }

        Task<MessagesResponse> LoadChat(Chat chat);

        Task SendMessage(string message);

        Task<ChatVM> SendFirstUserMessage(string message, string chatId, string chatType);

        void OpenNewChat();
    }
}
