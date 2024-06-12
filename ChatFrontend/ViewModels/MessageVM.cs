using ChatFrontend.Models;
using ShopContent.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace ChatFrontend.ViewModels
{
    public class MessageVM : ViewModel
    {
        private Message _message;
        private Chat _chat;
        private ICollection<User> _members;

        public Message Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public Chat Chat
        {
            get => _chat;
            set
            {
                _chat = value;
                OnPropertyChanged(nameof(Chat));
            }
        }
        public User Sender
        {
            get => _members.FirstOrDefault(u => u.Id == _message.FromId);
        }

        public MessageVM(Message message, Chat chat, ICollection<User> members)
        {
            Message = message;
            Chat = chat;
            _members = members;
        }
    }
}
