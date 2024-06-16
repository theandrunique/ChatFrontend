using ChatFrontend.Models;
using ChatFrontend.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IChatsService
    {
        ObservableCollection<ChatVM> Chats { get; }

        ObservableCollection<MessageVM> Messages { get; }

        Task OpenChat(Chat chat);

        Task SendMessage(string message);

        Task SendMessage(string message, string userId);
    }
}
