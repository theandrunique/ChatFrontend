using ChatFrontend.Models;
using ShopContent.ViewModels.Base;

namespace ChatFrontend.ViewModels
{
    public class MessageVM : ViewModel
    {
        private Message _message;
        private User _sender;

        public Message Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public User Sender
        {
            get => _sender;
            set
            {
                _sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        public MessageVM(Message message, User sender)
        {
            Message = message;
            _sender = sender;
        }
    }
}
