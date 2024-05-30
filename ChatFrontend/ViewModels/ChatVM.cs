using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class ChatVM : ViewModel
    {
        public ICommand SendMessageCommand { get; }

        string _message;
        ObservableCollection<MessageVM> _messages = new ObservableCollection<MessageVM>();

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ObservableCollection<MessageVM> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public ChatVM()
        {
            SendMessageCommand = new LambdaCommand(ExecuteSendMessage);
        }
        bool kek = false;
        private void ExecuteSendMessage(object obj)
        {
            Messages.Add(new MessageVM()
            {
                Username = "theandru",
                MessageContent = Message,
                Timestamp = DateTime.Now,
                ImageUrl = "https://avatars.githubusercontent.com/u/127850940?v=4",
                IsSentByCurrentUser = kek,
            });
            kek = !kek;
            Messages = Messages;
            Message = string.Empty;
        }
    }
}
