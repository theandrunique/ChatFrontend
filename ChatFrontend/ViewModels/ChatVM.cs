using ChatFrontend.Models;
using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class ChatVM : ViewModel
    {
        private Chat _chat;

        public Chat Chat
        {
            get => _chat;
            set
            {
                _chat = value;
                OnPropertyChanged(nameof(Chat));
            }
        }

        public ChatVM(Chat chat)
        {
            _chat = chat;
        }
    }
}
