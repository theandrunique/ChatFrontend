using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ShopContent.ViewModels.Base;
using System.Linq;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class MessengerVM : ViewModel
    {
        private readonly IChatService _chatService;

        private ChatVM _selectedChat;
        private string _messageInput;
        private User _currentUser;

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

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public ICommand SendMessageCommand { get; set; }

        public MessengerVM(IChatService chatService, AppState appState)
        {
            _chatService = chatService;
            CurrentUser = appState.User;

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

            if (SelectedChat.IsNew)
            {
                var newChatVM = await ChatService.SendFirstUserMessage(MessageInput, SelectedChat.Chat.Id, SelectedChat.Chat.Type);
                SelectedChat = newChatVM;
            }
            else
                await ChatService.SendMessage(MessageInput);

            MessageInput = string.Empty;
        }

        private void OnChatSelectedChanged()
        {
            if (SelectedChat == null)
                return;

            ChatService.LoadChat(SelectedChat.Chat);
        }

        public async void OpenNewChat(User user)
        {
            var existedChat = ChatService.Chats.FirstOrDefault(c => c.Chat.Id == user.Id);

            if (existedChat != null)
            {
                SelectedChat = existedChat;
                await ChatService.LoadChat(SelectedChat.Chat);
                return;
            }
            else
            {
                ChatService.OpenNewChat();
                SelectedChat = new ChatVM(new Models.Chat()
                {
                    Id = user.Id,
                    Name = user.Username,
                    ImageUrl = user.ImageUrl,
                    Type = "private",
                })
                {
                    IsNew = true,
                };
            }
        }
    }
}
