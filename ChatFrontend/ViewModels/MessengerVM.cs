using ChatFrontend.Services.Base;
using ShopContent.Commands;
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
                OnChatSelectedChanged(SelectedChat.Chat.Id);
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

        public MessengerVM(IChatService chatService)
        {
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

        private void OnChatSelectedChanged(string chatId)
        {
            if (SelectedChat == null)
                return;

            ChatService.LoadChat(chatId);
        }

        public async void OpenNewChat(string id)
        {
            var messages = await ChatService.LoadChat(id);

            var chat = ChatService.Chats.First(c => c.Chat.Id == messages.Chat.Id);

            SelectedChat = chat;
        }
    }
}
