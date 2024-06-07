﻿using ChatFrontend.Core;
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
        public ObservableCollection<Message> Messages { get; private set; } = new ObservableCollection<Message>();
        public ObservableCollection<ChatVM> Chats { get; private set; } = new ObservableCollection<ChatVM>();
        

        private readonly IMessengerService _messengerService;
        private readonly IJsonService _jsonService;
        private WebSocket _webSocket;
        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        private int _currentSelectedChatIndex = -1;

        public ChatService(IMessengerService messengerService, IJsonService jsonService, AuthenticationState authenticationState)
        {
            if (authenticationState.IsAuthenticated == false)
                throw new Exception("Can't use without authentication");

            _messengerService = messengerService;
            _jsonService = jsonService;
            _webSocket = new WebSocket($"{MessengerServiceSettings.WebSocketUrl}?access_token={authenticationState.AccessToken}");

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
                if (Chats[_currentSelectedChatIndex].Chat.Id == Chats[index].Chat.Id)
                    Messages.Add(newMessage.Message);
            });
        }

        public async void LoadChat(string chatId)
        {
            MessagesResponse messagesResponse = await _messengerService.GetMessages(chatId, 20, 0);

            dispatcher.Invoke(() =>
            {
                Messages.Clear();
                foreach (var chat in messagesResponse.Messages)
                    Messages.Add(chat);
                _currentSelectedChatIndex = Chats.IndexOf(Chats.First(c => c.Chat.Id == messagesResponse.Chat.Id));
            });
        }

        public async Task SendMessage(string message)
        {
            var newMessage = await _messengerService.SendMessage(Chats[_currentSelectedChatIndex].Chat.Id, message);
        }

        private async void LoadChats()
        {
            List<Chat> chatsResponse = await _messengerService.GetChats();

            dispatcher.Invoke(() =>
            {
                foreach (var chat in chatsResponse)
                    Chats.Add(new ChatVM(chat));
            });
        }
    }
}