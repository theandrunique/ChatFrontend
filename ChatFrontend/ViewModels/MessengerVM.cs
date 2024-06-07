using ChatFrontend.Models;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private ObservableCollection<Chat> _chats = new ObservableCollection<Chat>();
        private Chat _selectedChat;
        private string _messageInput;
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();

        public NavigationMenuVM NavigationMenuVM { get; }

        public Chat SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;
                OnPropertyChanged(nameof(SelectedChat));
                OnChatSelectedChanged();
            }
        }

        public ObservableCollection<Chat> Chats
        {
            get => _chats;
            set
            {
                _chats = value;
                OnPropertyChanged(nameof(Chats));
            }
        }

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public string MessageInput
        {
            get => _messageInput;
            set
            {
                _messageInput = value;
                OnPropertyChanged(nameof(MessageInput));
            }
        }

        public ICommand SendMessageCommand { get; set; }

        public MessengerVM(NavigationMenuVM navigationMenuVM)
        {
            NavigationMenuVM = navigationMenuVM;
            SendMessageCommand = new LambdaCommand(ExecuteSendMessageCommand, CanExecuteSendMessage);

            Chats.Add(new Chat()
            {
                Id = "133",
                Name = "Chat 1",
                LastMessage = new Message()
                {
                    FromId = "133",
                    Text = "Test message Test Test message Test",
                },
            });
            Chats.Add(new Chat()
            {
                Id = "133",
                Name = "Chat 1",
                LastMessage = new Message()
                {
                    FromId = "133",
                    Text = "loxz",
                },
            });
            Chats.Add(new Chat()
            {
                Id = "133",
                Name = "Chat 1",
                LastMessage = new Message()
                {
                    FromId = "133",
                    Text = "kek",
                },
            });
        }

        private bool CanExecuteSendMessage(object arg)
        {
            return !string.IsNullOrWhiteSpace(MessageInput);
        }

        private void ExecuteSendMessageCommand(object obj)
        {
            MessageBox.Show(MessageInput);
        }

        private void OnChatSelectedChanged()
        {
            Messages = new ObservableCollection<Message>(new List<Message>()
            {
                SelectedChat.LastMessage,
            });
        }
    }
}
