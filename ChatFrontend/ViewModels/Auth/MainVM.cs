using ShopContent.Commands;
using ShopContent.ViewModels.Base;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatFrontend.ViewModels.Auth
{
    public class MainVM : ViewModel
    {
        public LoginVM LoginVM { get; } = new LoginVM();
        public SignUpVM SignUpVM { get; } = new SignUpVM();
        UserControl _currentPage;
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }

        public UserControl CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public MainVM()
        {
            NavigateToLoginCommand = new LambdaCommand(NavigateToLogin);
            NavigateToSignUpCommand = new LambdaCommand(NavigateToSignUp);

            CurrentPage = new Views.UserControls.Auth();
        }

        private void NavigateToLogin(object obj)
        {
            CurrentPage = new Views.UserControls.Auth();
        }

        private void NavigateToSignUp(object obj)
        {
            CurrentPage = new Views.UserControls.Register();
        }
    }
}
