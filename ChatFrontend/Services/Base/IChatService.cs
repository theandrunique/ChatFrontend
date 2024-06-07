using ChatFrontend.Models;
using ChatFrontend.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IChatService
    {
        ObservableCollection<Message> Messages { get; }

        ObservableCollection<ChatVM> Chats { get; }

        void LoadChat(string chatId);

        Task SendMessage(string message);
    }
}
