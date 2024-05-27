using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Auth
{
    public class LoginVM : ViewModel
    {
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
        public LoginVM()
        {
            LoginCommand = new LambdaCommand(ExecuteLogin);
        }

        private void ExecuteLogin(object obj)
        {
            MessageBox.Show($"{Login} {Password}");
        }
    }
}
