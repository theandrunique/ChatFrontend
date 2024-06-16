using ChatFrontend.Core;
using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ChatFrontend.Services.Responses;
using System;
using WebSocketSharp;

namespace ChatFrontend.Services
{
    public class WebSocketService : IWebSocketService
    {
        readonly IJsonService _jsonService;
        readonly IAuthService _authService;

        private WebSocket _webSocket;

        public event EventHandler<NewMessageEventArgs> OnMessage;

        public WebSocketService(Settings settings, IAuthService authService, IJsonService jsonService)
        {
            _authService = authService;
            _jsonService = jsonService;
            InitSocket(settings);

        }
        private async void InitSocket(Settings settings)
        {
            _webSocket = new WebSocket($"{settings.MessengerWebSocketUrl}?access_token={await _authService.GetAccessToken()}");
            _webSocket.OnMessage += OnMessageReceived;
            _webSocket.Connect();
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            string message = e.Data;

            var newMessage = _jsonService.Deserialize<NewMessageResponse>(message);

            OnMessage?.Invoke(this, new NewMessageEventArgs() { Message = newMessage.Message, Chat = newMessage.Chat });
        }
    }
    public class NewMessageEventArgs
    {
        public Message Message { get; set; }
        public Chat Chat { get; set; }
    }
}
