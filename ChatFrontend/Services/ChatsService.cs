using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ChatFrontend.Services
{
    public class ChatsService : IChatsService
    {
        public ObservableCollection<ChatVM> Chats { get; } = new ObservableCollection<ChatVM>();
        public ObservableCollection<MessageVM> Messages { get => _messagesService.Messages; }

        private readonly IMessengerService _messengerService;
        private readonly IMessagesService _messagesService;
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        private Dictionary<string, ChatVM> _chats = new Dictionary<string, ChatVM>();

        public ChatsService(IMessengerService messengerService, IMessagesService messagesService, IWebSocketService webSocketService)
        {
            _messengerService = messengerService;
            _messagesService = messagesService;

            webSocketService.OnMessage += ReceiveMessage;
            LoadChats();
        }

        public async Task OpenChat(Chat chat)
        {
            await _messagesService.LoadChat(chat.Id, chat.Type);
        }

        public async Task SendMessage(string message)
        {
            var newMessage = await _messagesService.SendMessage(message);
            if (_chats.ContainsKey(newMessage.Chat.Id))
                _chats[newMessage.Chat.Id].Chat = newMessage.Chat;
            else
                throw new Exception("Chat not found");
        }

        public async Task SendMessage(string message, string userId)
        {
            var newMessage = await _messagesService.SendMessage(message, userId);
            if (_chats.ContainsKey(newMessage.Chat.Id))
                _chats[newMessage.Chat.Id].Chat = newMessage.Chat;
            else
                _chats.Add(userId, new ChatVM(newMessage.Chat));
        }

        private void ReceiveMessage(object sender, NewMessageEventArgs e)
        {
            if (_chats.ContainsKey(e.Chat.Id))
                _chats[e.Chat.Id].Chat = e.Chat;
            else
            {
                var chatVM = new ChatVM(e.Chat);
                _chats.Add(e.Chat.Id, chatVM);
                dispatcher.Invoke(() => Chats.Add(chatVM));
            }
        }

        private async Task LoadChats()
        {
            List<Chat> chatsResponse = await _messengerService.GetChats();

            dispatcher.Invoke(() =>
            {
                Chats.Clear();
                foreach (var chat in chatsResponse)
                {
                    var chatVM = new ChatVM(chat);
                    _chats.Add(chat.Id, chatVM);
                    Chats.Add(chatVM);
                }
            });
        }
    }
}
