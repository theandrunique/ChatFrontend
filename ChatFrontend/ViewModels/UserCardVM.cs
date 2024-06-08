using ChatFrontend.Models;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class UserCardVM : ViewModel
    {
        private User _user;

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand SendMessageCommand { get; }

        public UserCardVM(User user)
        {
            User = user;
            SendMessageCommand = new LambdaCommand(ExecuteSendMessage);
        }

        private void ExecuteSendMessage(object obj)
        {
            MessageBox.Show(User.Email);
        }
    }
}
