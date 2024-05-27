using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Auth
{
    public class SignUpVM : ViewModel
    {
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
        public SignUpVM()
        {
            SignUpCommand = new LambdaCommand(ExecuteSignUp);
        }

        private void ExecuteSignUp(object obj)
        {
            MessageBox.Show($"{Username} {Email} {Password} {ConfirmPassword}");
        }
    }
}
