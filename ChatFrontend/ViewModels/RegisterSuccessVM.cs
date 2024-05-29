using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class RegisterSuccessVM : ViewModel
    {
        private readonly INavigationService _navigation;
        public ICommand NavigateToLoginCommand { get; }
        public ICommand SendConfirmEmailCommand { get; }
        public RegisterSuccessVM(INavigationService navigation)
        {
            _navigation = navigation;
            NavigateToLoginCommand = new LambdaCommand(NavigateToLogin);
            SendConfirmEmailCommand = new LambdaCommand(SendConfirmEmail);
        }

        private void SendConfirmEmail(object obj)
        {
            throw new NotImplementedException();
        }

        private void NavigateToLogin(object obj)
        {
            _navigation.NavigateTo<LoginVM>();
        }
    }
}
