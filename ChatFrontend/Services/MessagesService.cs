using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using ChatFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ChatFrontend.Services
{
    public class MessagesService : IMessagesService
    {
        readonly IMessengerService _messengerService;
        readonly IUsersService _usersService;
        readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public ObservableCollection<MessageVM> Messages { get; } = new ObservableCollection<MessageVM>();

        private Dictionary<string, List<MessageVM>> _chatMessages = new Dictionary<string, List<MessageVM>>();

        private string CurrentChatId { get; set; }
        private string CurrentChatType { get; set; }

        public MessagesService(IMessengerService messengerService, IWebSocketService webSocketService, IUsersService usersService)
        {
            _usersService = usersService;
            _messengerService = messengerService;
            webSocketService.OnMessage += OnMessage;
        }

        private async void OnMessage(object sender, NewMessageEventArgs e)
        {
            if (!_chatMessages.ContainsKey(e.Chat.Id))
            {
                await GetMessages(e.Chat.Id, e.Chat.Type);
            } 
            else
            {
                User fromUser = await _usersService.GetUserById(e.Message.FromId);

                if (_chatMessages.ContainsKey(e.Chat.Id))
                    _chatMessages[e.Chat.Id].Add(new MessageVM(e.Message, fromUser));

                if (CurrentChatId == e.Chat.Id)
                    dispatcher.Invoke(() => Messages.Add(new MessageVM(e.Message, fromUser)));
            }
        }

        public async Task LoadChat(string chatId, string chatType)
        {
            CurrentChatId = chatId;
            CurrentChatType = chatType;

            List<MessageVM> messages = await GetMessages(chatId, chatType);

            dispatcher.Invoke(() =>
            {
                Messages.Clear();
                foreach (var message in messages)
                    Messages.Add(message);
            });
        }

        public async Task<NewMessageResponse> SendMessage(string message)
        {
            NewMessageResponse newMessage;
            if (CurrentChatType == "group")
                newMessage = await _messengerService.SendMessage(CurrentChatId, message);
            else if (CurrentChatType == "private")
                newMessage = await _messengerService.SendPrivateMessage(CurrentChatId, message);
            else
                throw new Exception("Unexpected behavior");

            User fromUser = await _usersService.GetUserById(newMessage.Message.FromId);

            var newMessageVM = new MessageVM(newMessage.Message, fromUser);
            _chatMessages[CurrentChatId].Add(newMessageVM);
            dispatcher.Invoke(() => Messages.Add(newMessageVM));

            return newMessage;
        }

        public async Task<NewMessageResponse> SendMessage(string message, string userId)
        {
            var newMessage = await _messengerService.SendPrivateMessage(userId, message);
            User fromUser = await _usersService.GetUserById(newMessage.Message.FromId);

            var newMessageVM = new MessageVM(newMessage.Message, fromUser);
            _chatMessages.Add(userId, new List<MessageVM>() { newMessageVM });
            Messages.Add(newMessageVM);
            return newMessage;
        }

        private async Task<List<MessageVM>> GetMessages(string chatId, string chatType)
        {
            if (_chatMessages.ContainsKey(chatId))
            {
                return _chatMessages[chatId];
            }
            MessagesResponse messagesResponse;

            try
            {
                if (chatType == "group")
                    messagesResponse = await _messengerService.GetMessages(chatId, 20, 0);
                else if (chatType == "private")
                    messagesResponse = await _messengerService.GetPrivateMessages(chatId, 20, 0);
                else
                    throw new Exception("Unexpected behavior");
            }
            catch (ErrorResponse errorResponse)
            {
                return new List<MessageVM>();
            }

            List<MessageVM> messagesVM = new List<MessageVM>();

            foreach (var message in messagesResponse.Messages)
            {
                User fromUser = await _usersService.GetUserById(message.FromId);
                messagesVM.Add(new MessageVM(message, fromUser));
            }

            _chatMessages.Add(chatId, messagesVM);
            return messagesVM;
        }
    }
}
