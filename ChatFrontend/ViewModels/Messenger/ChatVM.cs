using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Messenger
{
    public class ChatVM : ViewModel
    {
        public ICommand SendMessageCommand { get; }

        string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ChatVM()
        {
            SendMessageCommand = new LambdaCommand(ExecuteSendMessage);
        }

        private void ExecuteSendMessage(object obj)
        {
            MessageBox.Show(Message);
        }
    }
}
