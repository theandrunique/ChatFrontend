using ChatFrontend.Services.Base;
using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels
{
    public class SignUpVM : ViewModel
    {
        private readonly INavigationService _navigation;
        string _username = "";
        string _email = "";
        string _password = "";
        string _confirmPassword = "";
        public string Username
        {
            get => _username; set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get => _email; set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
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
        public string ConfirmPassword
        {
            get => _confirmPassword; set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public ICommand SignUpCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public SignUpVM(INavigationService navigation)
        {
            _navigation = navigation;
            SignUpCommand = new LambdaCommand(ExecuteSignUp);
            NavigateToLoginCommand = new LambdaCommand(NavigateToLogin);
        }
        private void NavigateToLogin(object obj)
        {
            _navigation.NavigateTo<LoginVM>();
        }

        private void ExecuteSignUp(object obj)
        {
            MessageBox.Show($"{Username} {Email} {Password} {ConfirmPassword}");
            _navigation.NavigateTo<RegisterSuccessVM>();
        }
    }
}
