using ChatFrontend.Services.Responses;
using ChatFrontend.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatFrontend.Services.Base
{
    public interface IMessagesService
    {
        ObservableCollection<MessageVM> Messages { get; }

        Task LoadChat(string chatId, string chatType);

        Task<NewMessageResponse> SendMessage(string message);

        Task<NewMessageResponse> SendMessage(string message, string userId);
    }
}
