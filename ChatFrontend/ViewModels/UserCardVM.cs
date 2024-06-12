using ChatFrontend.Models;
using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class UserCardVM : ViewModel
    {
        private readonly ISessionNavigationService _sessionNavigationService;

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

        public UserCardVM(User user, ISessionNavigationService sessionNavigationService)
        {
            _sessionNavigationService = sessionNavigationService;

            User = user;
            SendMessageCommand = new LambdaCommand(ExecuteSendMessage);
        }

        private void ExecuteSendMessage(object obj)
        {
            var messengerViewModel = (MessengerVM)_sessionNavigationService.ViewModelFactory.Invoke(typeof(MessengerVM));
            messengerViewModel.OpenNewChat(User);
            _sessionNavigationService.NavigateTo<MessengerVM>();
        }
    }
}
