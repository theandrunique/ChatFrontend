using ShopContent.ViewModels.Base;
using System;

namespace ChatFrontend.ViewModels
{
    public class MessageVM : ViewModel
    {
        string _username;
        DateTime _timestamp;
        string _messageContent;
        string _imageUrl;
        bool _isSentByCurrentUser;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public DateTime Timestamp
        {
            get => _timestamp; set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
        }
        public string MessageContent
        {
            get => _messageContent;
            set
            {
                _messageContent = value;
                OnPropertyChanged(nameof(MessageContent));
            }
        }
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        public bool IsSentByCurrentUser
        {
            get => _isSentByCurrentUser;
            set
            {
                _isSentByCurrentUser = value;
                OnPropertyChanged(nameof(IsSentByCurrentUser));
            }
        }

        public MessageVM()
        {

        }
    }
}
