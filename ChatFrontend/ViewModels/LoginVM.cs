using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class LoginVM : ViewModel
    {
        private readonly INavigationService _navigation;
        public ICommand NavigateToSignUpCommand { get; }
        string _login = "";
        string _password = "";

        public string Login
        {
            get => _login; set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _password; set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand LoginCommand { get; }
        public LoginVM(INavigationService navigation)
        {
            _navigation = navigation;
            LoginCommand = new LambdaCommand(ExecuteLogin);
            NavigateToSignUpCommand = new LambdaCommand(NavigateToSignUp);
        }

        private void NavigateToSignUp(object obj)
        {
            _navigation.NavigateTo<SignUpVM>();
        }
        private void ExecuteLogin(object obj)
        {
            MessageBox.Show($"{Login} {Password}");

            _navigation.NavigateTo<MessengerVM>();
        }
    }
}
