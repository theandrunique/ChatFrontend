using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using ChatFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using WebSocketSharp;

namespace ChatFrontend.Services
{
    public class ChatService : IChatService
    {
        public ObservableCollection<MessageVM> Messages { get; } = new ObservableCollection<MessageVM>();
        public ObservableCollection<ChatVM> Chats { get; } = new ObservableCollection<ChatVM>();

        public List<User> CurrentChatMembers { get; private set; } = new List<User>();

        private readonly IMessengerService _messengerService;
        private readonly IJsonService _jsonService;
        private readonly IAuthService _authService;
        private WebSocket _webSocket;
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        private int _currentSelectedChatIndex = -1;

        public ChatService(IMessengerService messengerService, IJsonService jsonService, AppState appState, IAuthService authService, Settings settings)
        {
            if (appState.IsAuthenticated == false)
                throw new Exception("Can't use without authentication");

            _messengerService = messengerService;
            _jsonService = jsonService;
            _authService = authService;
            _webSocket = new WebSocket($"{settings.MessengerWebSocketUrl}?access_token={appState.AccessToken}");

            _webSocket.OnMessage += ReceiveMessage;
            _webSocket.Connect();

            LoadChats();
        }

        private void ReceiveMessage(object sender, MessageEventArgs e)
        {
            var newMessage = _jsonService.Deserialize<NewMessageResponse>(e.Data);

            dispatcher.Invoke(() =>
            {
                int index = Chats.IndexOf(Chats.FirstOrDefault(c => c.Chat.Id == newMessage.Chat.Id));
                if (index == -1)
                {
                    Chats.Add(new ChatVM(newMessage.Chat));
                }
                else
                {
                    Chats[index].Chat = newMessage.Chat;
                }
                if (_currentSelectedChatIndex != -1 && Chats[_currentSelectedChatIndex].Chat.Id == Chats[index].Chat.Id)
                    Messages.Add(new MessageVM(newMessage.Message, newMessage.Chat, CurrentChatMembers));
            });
        }

        public void OpenNewChat()
        {
            dispatcher.Invoke(() => Messages.Clear());
            _currentSelectedChatIndex = -1;
        }

        public async Task<MessagesResponse> LoadChat(Chat chat)
        {
            MessagesResponse messagesResponse;

            if (chat.Type == "group")
                messagesResponse = await _messengerService.GetMessages(chat.Id, 20, 0);
            else if (chat.Type == "private")
                messagesResponse = await _messengerService.GetPrivateMessages(chat.Id, 20, 0);
            else
                throw new Exception("Unexpected behavior");

            var membersResponse = await _authService.GetUsers(messagesResponse.Chat.Members);
            CurrentChatMembers = membersResponse.Result;

            dispatcher.Invoke(() =>
            {
                Messages.Clear();
                foreach (var message in messagesResponse.Messages)
                    Messages.Add(new MessageVM(message, messagesResponse.Chat, CurrentChatMembers));
            });
            _currentSelectedChatIndex = Chats.IndexOf(Chats.First(c => c.Chat.Id == messagesResponse.Chat.Id));
            return messagesResponse;
        }

        public async Task<ChatVM> SendFirstUserMessage(string message, string chatId, string chatType)
        {
            NewMessageResponse newMessage;
            if (chatType == "group")
                newMessage = await _messengerService.SendMessage(chatId, message);
            else if (chatType == "private")
                newMessage = await _messengerService.SendPrivateMessage(chatId, message);
            else
                throw new Exception("Unexpected behavior");

            ChatVM newChatVM = new ChatVM(newMessage.Chat);

            var membersResponse = await _authService.GetUsers(newMessage.Chat.Members);
            CurrentChatMembers = membersResponse.Result;

            dispatcher.Invoke(() =>
            {
                if (newMessage.Chat.MessageCount == 1)
                {
                    dispatcher.Invoke(() => Chats.Add(newChatVM));
                }
                Messages.Add(new MessageVM(newMessage.Message, newMessage.Chat, CurrentChatMembers));
                var chat = Chats.FirstOrDefault(c => c.Chat.Id == newMessage.Chat.Id);
                chat.Chat = newMessage.Chat;
            });
            return newChatVM;
        }

        public async Task SendMessage(string message)
        {
            NewMessageResponse newMessage;
            if (Chats[_currentSelectedChatIndex].Chat.Type == "group")
                newMessage = await _messengerService.SendMessage(Chats[_currentSelectedChatIndex].Chat.Id, message);
            else if (Chats[_currentSelectedChatIndex].Chat.Type == "private")
                newMessage = await _messengerService.SendPrivateMessage(Chats[_currentSelectedChatIndex].Chat.Id, message);
            else
                throw new Exception("Unexpected behavior");

            dispatcher.Invoke(() =>
            {
                Messages.Add(new MessageVM(newMessage.Message, newMessage.Chat, CurrentChatMembers));
                var chat = Chats.FirstOrDefault(c => c.Chat.Id == newMessage.Chat.Id);
                chat.Chat = newMessage.Chat;
            });
        }

        private async Task LoadChats()
        {
            List<Chat> chatsResponse = await _messengerService.GetChats();

            dispatcher.Invoke(() =>
            {
                Chats.Clear();
                foreach (var chat in chatsResponse)
                    Chats.Add(new ChatVM(chat));
            });
        }
    }
}
