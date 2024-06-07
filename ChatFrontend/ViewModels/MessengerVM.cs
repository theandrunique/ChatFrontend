using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private readonly IChatService _chatService;

        private ChatVM _selectedChat;
        private string _messageInput;

        public NavigationMenuVM NavigationMenuVM { get; }

        public IChatService ChatService
        {
            get => _chatService;
        }

        public ChatVM SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;
                OnPropertyChanged(nameof(SelectedChat));
                OnChatSelectedChanged();
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

        public MessengerVM(NavigationMenuVM navigationMenuVM, IChatService chatService)
        {
            NavigationMenuVM = navigationMenuVM;
            _chatService = chatService;

            SendMessageCommand = new LambdaCommand(ExecuteSendMessageCommand, CanExecuteSendMessage);
        }

        private bool CanExecuteSendMessage(object arg)
        {
            return !string.IsNullOrWhiteSpace(MessageInput);
        }

        private async void ExecuteSendMessageCommand(object obj)
        {
            if (SelectedChat == null)
                return;

            await ChatService.SendMessage(MessageInput);
            MessageInput = string.Empty;
        }

        private void OnChatSelectedChanged()
        {
            if (SelectedChat == null)
                return;

            ChatService.LoadChat(SelectedChat.Chat.Id);
        }
    }
}
