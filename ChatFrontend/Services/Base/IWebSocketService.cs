using System;

namespace ChatFrontend.Services.Base
{
    public interface IWebSocketService
    {
        event EventHandler<NewMessageEventArgs> OnMessage;
    }
}
